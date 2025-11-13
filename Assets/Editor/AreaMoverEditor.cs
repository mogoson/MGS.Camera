/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AreaMoverEditor.cs
 *  Description  :  Custom editor for MouseTranslate.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/9/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  1.1
 *  Date         :  6/27/2018
 *  Description  :  Optimize display of node.
 *************************************************************************/

using MGS.Editors;
using UnityEditor;
using UnityEngine;

namespace MGS.Cameras.Editors
{
    [CustomEditor(typeof(AreaMover), true)]
    [CanEditMultipleObjects]
    public class AreaMoverEditor : SceneEditor
    {
        #region Field and Property
        protected AreaMover Target { get { return target as AreaMover; } }
        protected Vector3 offset;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            if (Target.areaSettings.center == null)
            {
                return;
            }
            offset = Target.transform.position - Target.areaSettings.center.position;
        }

        protected virtual void OnSceneGUI()
        {
            if (Target.areaSettings.center == null)
            {
                return;
            }

            if (!Application.isPlaying)
            {
                Target.transform.position = Target.areaSettings.center.position + offset;
            }
            DrawSceneGizmos();

            if (Target.targetCamera == null)
            {
                return;
            }

            DrawSphereArrow(Target.transform.position, Target.targetCamera.position, NodeSize);
            DrawSceneGUI();
        }

        protected void DrawSceneGizmos()
        {
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
        }

        protected void DrawSceneGUI()
        {
            var sceneRect = SceneView.lastActiveSceneView.position;
            var rect = new Rect(3, sceneRect.height - 73, 225, 45);

            GUI.color = Color.white;
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Current Offset", "Window");
            if (Application.isPlaying)
            {
                EditorGUILayout.Vector3Field(string.Empty, Target.CurrentOffset);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                offset = EditorGUILayout.Vector3Field(string.Empty, offset);
                if (EditorGUI.EndChangeCheck())
                {
                    MarkSceneDirty();
                }
            }
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}