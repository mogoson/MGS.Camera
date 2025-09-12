/*************************************************************************
 *  Copyright © 2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SceneEditor.cs
 *  Description  :  Define scene editor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

namespace MGS.Cameras.Editors
{
    public class SceneEditor : Editor
    {
#if UNITY_5_5_OR_NEWER
        protected readonly Handles.CapFunction CircleCap = Handles.CircleHandleCap;
        protected readonly Handles.CapFunction SphereCap = Handles.SphereHandleCap;
#else
        protected readonly Handles.DrawCapFunction CircleCap = Handles.CircleCap;
        protected readonly Handles.DrawCapFunction SphereCap = Handles.SphereCap;
#endif
        protected readonly Color Blue = new Color(0, 1, 1, 1);
        protected readonly Color TransparentBlue = new Color(0, 1, 1, 0.1f);
        protected const float NodeSize = 0.125f;

        protected void DrawSphereCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
            {
                SphereCap(0, position, rotation, size, EventType.Repaint);
            }
#else
            SphereCap(0, position, rotation, size);
#endif
        }

        protected void DrawAdaptiveSphereCap(Vector3 position, Quaternion rotation, float size)
        {
            DrawSphereCap(position, rotation, size * GetHandleSize(position));
        }

        protected void DrawSphereArrow(Vector3 start, Vector3 end, float size, string text = "")
        {
            Handles.DrawLine(start, end);
            DrawAdaptiveSphereCap(end, Quaternion.identity, size);
            Handles.Label(end, text);
        }

        protected void DrawSphereArrow(Vector3 start, Vector3 direction, float length, float size, string text = "")
        {
            DrawSphereArrow(start, start + direction.normalized * length, size, text);
        }

        protected void DrawPositionHandle(Transform transform)
        {
            EditorGUI.BeginChangeCheck();
            var position = Handles.PositionHandle(transform.position, GetPivotRotation(transform));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(transform, "CHANGE_POSITION_HANDLE");
                transform.position = position;
                MarkSceneDirty();
            }
        }

        protected float GetHandleSize(Vector3 position)
        {
            return HandleUtility.GetHandleSize(position);
        }

        protected Quaternion GetPivotRotation(Transform transform)
        {
            if (Tools.pivotRotation == PivotRotation.Local)
            {
                return transform.rotation;
            }
            else
            {
                return Quaternion.identity;
            }
        }

        protected void MarkSceneDirty()
        {
#if UNITY_5_3_OR_NEWER
            var scene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(scene);
#else
            EditorApplication.MarkSceneDirty();
#endif
        }
    }
}