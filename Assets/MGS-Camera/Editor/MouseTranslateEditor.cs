/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: MouseTranslateEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 4/28/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.      MouseTranslateEditor        Ignore.
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

    [CustomEditor(typeof(MouseTranslate), true)]
    [CanEditMultipleObjects]
    public class MouseTranslateEditor : CameraEditor
    {
        #region Property and Field
        protected MouseTranslate script { get { return target as MouseTranslate; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            if (script.areaSettings.center == null)
                return;
            DrawPositionHandle(script.areaSettings.center);

            var widthOffset = Vector3.right * script.areaSettings.width;
            var lengthOffset = Vector3.forward * script.areaSettings.length;

            var verts = new Vector3[4];
            verts[0] = script.areaSettings.center.position + widthOffset + lengthOffset;
            verts[1] = script.areaSettings.center.position - widthOffset + lengthOffset;
            verts[2] = script.areaSettings.center.position - widthOffset - lengthOffset;
            verts[3] = script.areaSettings.center.position + widthOffset - lengthOffset;

            GUI.color = Handles.color = blue;
            Handles.Label(script.areaSettings.center.position, "Center");
            Handles.SphereCap(0, script.areaSettings.center.position, Quaternion.identity, nodeSize);
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
            Handles.DrawSolidRectangleWithOutline(verts, transparentBlue, blue);

            var project = new Vector3(script.transform.position.x, script.areaSettings.center.position.y, script.transform.position.z);
            DrawArrow(script.transform.position, project, nodeSize, string.Empty, blue);
            Handles.DrawLine(new Vector3(verts[0].x, verts[0].y, script.transform.position.z), new Vector3(verts[1].x, verts[1].y, script.transform.position.z));
            Handles.DrawLine(new Vector3(script.transform.position.x, verts[0].y, verts[0].z), new Vector3(script.transform.position.x, verts[3].y, verts[3].z));

            if (script.targetCamera == null)
                return;
            DrawArrow(script.transform.position, script.targetCamera.position, nodeSize, string.Empty, blue);

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            GUI.color = Color.white;
            var rect = new Rect(10, Screen.height - 90, 170, 40);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Current Offset", "Window");
            GUILayout.Label((script.transform.position - script.areaSettings.center.position).ToString());
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}