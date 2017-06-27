/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: CameraEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 4/28/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         CameraEditor             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     4/28/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Camera
{
    using UnityEditor;
    using UnityEngine;

    public class CameraEditor : Editor
    {
        #region Property and Field
        protected Color blue = new Color(0, 1, 1, 1);
        protected Color transparentBlue = new Color(0, 1, 1, 0.1f);

        protected float nodeSize = 0.05f;
        protected float arrowLength = 0.75f;
        protected float lineLength = 10;
        protected float areaRadius = 0.5f;
        #endregion

        #region Protected Method
        protected virtual void DrawArrow(Vector3 start, Vector3 end, float size, string text, Color color)
        {
            var gC = GUI.color;
            var hC = Handles.color;

            GUI.color = Handles.color = color;

            Handles.DrawLine(start, end);
            Handles.SphereCap(0, end, Quaternion.identity, size);
            Handles.Label(end, text);

            GUI.color = gC;
            Handles.color = hC;
        }//DrawArrow()_end

        protected virtual void DrawArrow(Vector3 start, Vector3 direction, float length, float size, string text, Color color)
        {
            var end = start + direction.normalized * length;
            DrawArrow(start, end, size, text, color);
        }//DrawArrow()_end

        protected void DrawPositionHandle(Transform transform)
        {
            EditorGUI.BeginChangeCheck();
            var position = Handles.PositionHandle(transform.position, GetPivotRotation(transform));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(transform, "Change Position");
                transform.position = position;
            }//if()_end
        }//DrawP...()_end

        protected Quaternion GetPivotRotation(Transform transform)
        {
            if (Tools.pivotRotation == PivotRotation.Local)
                return transform.rotation;
            else
                return Quaternion.identity;
        }//GetP...()_end
        #endregion
    }//class_end
}//namespace_end