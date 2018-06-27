/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MouseTranslateEditor.cs
 *  Description  :  Custom editor for MouseTranslate.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/9/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  6/27/2018
 *  Description  :  Optimize display of node.
 *************************************************************************/

using Mogoson.UEditor;
using UnityEditor;
using UnityEngine;

namespace Mogoson.CameraExtension
{
    [CustomEditor(typeof(MouseTranslate), true)]
    [CanEditMultipleObjects]
    public class MouseTranslateEditor : GenericEditor
    {
        #region Field and Property
        protected MouseTranslate Target { get { return target as MouseTranslate; } }
        protected Vector3 offset;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            if (Target.areaSettings.center == null)
                return;
            offset = Target.transform.position - Target.areaSettings.center.position;
        }

        protected virtual void OnSceneGUI()
        {
            if (Target.areaSettings.center == null)
                return;

            if (!Application.isPlaying)
                Target.transform.position = Target.areaSettings.center.position + offset;

            DrawPositionHandle(Target.areaSettings.center);

            var widthOffset = Vector3.right * Target.areaSettings.width;
            var lengthOffset = Vector3.forward * Target.areaSettings.length;
            var verts = new Vector3[]
            {
                Target.areaSettings.center.position + widthOffset + lengthOffset,
                Target.areaSettings.center.position - widthOffset + lengthOffset,
                Target.areaSettings.center.position - widthOffset - lengthOffset,
                Target.areaSettings.center.position + widthOffset - lengthOffset
             };

            Handles.color = Blue;
            Handles.DrawSolidRectangleWithOutline(verts, TransparentBlue, Blue);
            Handles.DrawLine(new Vector3(verts[0].x, verts[0].y, Target.transform.position.z), new Vector3(verts[1].x, verts[1].y, Target.transform.position.z));
            Handles.DrawLine(new Vector3(Target.transform.position.x, verts[0].y, verts[0].z), new Vector3(Target.transform.position.x, verts[3].y, verts[3].z));
            Handles.Label(Target.areaSettings.center.position, "Center");

            DrawAdaptiveSphereCap(Target.areaSettings.center.position, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);

            var project = new Vector3(Target.transform.position.x, Target.areaSettings.center.position.y, Target.transform.position.z);
            DrawSphereArrow(Target.transform.position, project, NodeSize);

            if (Target.targetCamera == null)
                return;

            DrawSphereArrow(Target.transform.position, Target.targetCamera.position, NodeSize);
            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(10, Screen.height - 90, 225, 40);
            GUI.color = Color.white;
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Current Offset", "Window");
            if (Application.isPlaying)
                EditorGUILayout.Vector3Field(string.Empty, Target.CurrentOffset);
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