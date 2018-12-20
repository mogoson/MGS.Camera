/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AroundCameraEditor.cs
 *  Description  :  Custom editor for AroundCamera.
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

namespace Mogoson.UCamera
{
    [CustomEditor(typeof(AroundCamera), true)]
    [CanEditMultipleObjects]
    public class AroundCameraEditor : GenericEditor
    {
        #region Field and Property
        protected AroundCamera Target { get { return target as AroundCamera; } }
        protected Vector2 angles;
        protected float distance;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            if (Target.target == null)
            {
                return;
            }

            angles = Target.transform.eulerAngles;
            distance = Vector3.Distance(Target.transform.position, Target.target.position);
        }

        protected virtual void OnSceneGUI()
        {
            if (Target.target == null)
            {
                return;
            }

            if (!Application.isPlaying)
            {
                Target.transform.rotation = Quaternion.Euler(angles);
                Target.transform.position = Target.target.position - Target.transform.forward * distance;
            }

            Handles.color = Blue;
            DrawAdaptiveSphereCap(Target.target.position, Quaternion.identity, NodeSize);

            var direction = Target.transform.position - Target.target.position;
            DrawSphereArrow(Target.target.position, Target.transform.position, NodeSize);
            DrawSphereArrow(Target.target.position, direction, Target.distanceRange.min, NodeSize, "Min");
            DrawSphereArrow(Target.target.position, direction, Target.distanceRange.max, NodeSize, "Max");
            Handles.Label(Target.target.position, "Target");

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(10, Screen.height - 125, 225, 75);
            GUI.color = Color.white;
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Current Around", "Window");
            if (Application.isPlaying)
            {
                EditorGUILayout.Vector2Field("Angles", Target.CurrentAngles);
                EditorGUILayout.FloatField("Distance", Target.CurrentDistance);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                angles = EditorGUILayout.Vector2Field("Angles", angles);
                distance = EditorGUILayout.FloatField("Distance", distance);
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