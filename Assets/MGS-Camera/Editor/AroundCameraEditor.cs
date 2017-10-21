/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  AroundCameraEditor.cs
 *  Description  :  Custom editor for AroundCamera.
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
    [CustomEditor(typeof(AroundCamera), true)]
    [CanEditMultipleObjects]
    public class AroundCameraEditor : CameraEditor
    {
        #region Property and Field
        protected AroundCamera script { get { return target as AroundCamera; } }
        protected Vector2 angles;
        protected float distance;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            if (script.target == null)
                return;
            angles = script.transform.eulerAngles;
            distance = Vector3.Distance(script.transform.position, script.target.position);
        }

        protected virtual void OnSceneGUI()
        {
            if (script.target == null)
                return;

            if (!Application.isPlaying)
            {
                script.transform.rotation = Quaternion.Euler(angles);
                script.transform.position = script.target.position - script.transform.forward * distance;
            }

            GUI.color = Handles.color = blue;
            DrawSphereCap(script.target.position, Quaternion.identity, nodeSize);
            Handles.Label(script.target.position, "Target");
            var direction = (script.transform.position - script.target.position).normalized;
            DrawArrow(script.target.position, script.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(script.target.position, direction, script.distanceRange.min, nodeSize, "Min", blue);
            DrawArrow(script.target.position, direction, script.distanceRange.max, nodeSize, "Max", blue);

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            GUI.color = Color.white;
            var rect = new Rect(10, Screen.height - 125, 225, 75);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Current Around", "Window");
            if (Application.isPlaying)
            {
                EditorGUILayout.Vector2Field("Angles", script.currentAngles);
                EditorGUILayout.FloatField("Distance", script.currentDistance);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                angles = EditorGUILayout.Vector2Field("Angles", angles);
                distance = EditorGUILayout.FloatField("Distance", distance);
                if (EditorGUI.EndChangeCheck())
                    MarkSceneDirty();
            }
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}