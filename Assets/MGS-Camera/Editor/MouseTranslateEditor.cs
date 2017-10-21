/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  MouseTranslateEditor.cs
 *  Description  :  Custom editor for MouseTranslate.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/28/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.CameraExtension
{
    [CustomEditor(typeof(MouseTranslate), true)]
    [CanEditMultipleObjects]
    public class MouseTranslateEditor : CameraEditor
    {
        #region Property and Field
        protected MouseTranslate script { get { return target as MouseTranslate; } }
        protected Vector3 offset;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            if (script.areaSettings.center == null)
                return;
            offset = script.transform.position - script.areaSettings.center.position;
        }

        protected virtual void OnSceneGUI()
        {
            if (script.areaSettings.center == null)
                return;

            if (!Application.isPlaying)
                script.transform.position = script.areaSettings.center.position + offset;

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
            DrawSphereCap(script.areaSettings.center.position, Quaternion.identity, nodeSize);
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);
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
            var rect = new Rect(10, Screen.height - 90, 225, 40);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Current Offset", "Window");
            if (Application.isPlaying)
                EditorGUILayout.Vector3Field(string.Empty, script.currentOffset);
            else
            {
                EditorGUI.BeginChangeCheck();
                offset = EditorGUILayout.Vector3Field(string.Empty, offset);
                if (EditorGUI.EndChangeCheck())
                    MarkSceneDirty();
            }
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}