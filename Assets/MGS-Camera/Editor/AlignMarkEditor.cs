/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AlignMarkEditor.cs
 *  Description  :  Custom editor for AlignMark component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.CameraExtension
{
    [CustomEditor(typeof(AlignMark), true)]
    [CanEditMultipleObjects]
    public class AlignMarkEditor : BaseEditor
    {
        #region Field and Property
        protected AlignMark Target { get { return target as AlignMark; } }
        protected const string PreviewCameraName = "PreviewCamera";
        protected Camera previewCamera;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            var preview = GameObject.Find(PreviewCameraName);
            if (preview)
                previewCamera = preview.GetComponent<Camera>();
            else
            {
                previewCamera = new GameObject(PreviewCameraName) { hideFlags = HideFlags.HideAndDontSave }.AddComponent<Camera>();
                previewCamera.targetTexture = new RenderTexture(240, 180, 16) { hideFlags = HideFlags.DontSave };
            }
            previewCamera.Render();
        }

        protected virtual void OnDisable()
        {
            if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<AlignMark>() == null)
            {
                DestroyImmediate(previewCamera.gameObject, true);
                EditorUtility.UnloadUnusedAssetsImmediate(true);
            }
        }

        protected virtual void OnSceneGUI()
        {
            if (Target.alignTarget.center == null)
                return;

            DrawPositionHandle(Target.alignTarget.center);
            previewCamera.transform.rotation = Quaternion.Euler(Target.alignTarget.angles);
            previewCamera.transform.position = Target.alignTarget.center.position + previewCamera.transform.rotation * Vector3.back * Target.alignTarget.distance;

            Handles.color = Blue;
            var centerSize = HandleUtility.GetHandleSize(Target.alignTarget.center.position) * NodeSize;
            DrawSphereCap(Target.alignTarget.center.position, Quaternion.identity, centerSize);

            var cameraSize = HandleUtility.GetHandleSize(previewCamera.transform.position) * NodeSize;
            DrawSphereArrow(Target.alignTarget.center.position, previewCamera.transform.position, cameraSize, Blue, "Camera");

            var minPos = Target.alignTarget.center.position - previewCamera.transform.forward * Target.alignTarget.distanceRange.min;
            DrawSphereArrow(Target.alignTarget.center.position, minPos, HandleUtility.GetHandleSize(minPos) * NodeSize, Blue, "Min");

            var maxPos = Target.alignTarget.center.position - previewCamera.transform.forward * Target.alignTarget.distanceRange.max;
            DrawSphereArrow(Target.alignTarget.center.position, maxPos, HandleUtility.GetHandleSize(maxPos) * NodeSize, Blue, "Max");

            GUI.color = Blue;
            Handles.Label(Target.alignTarget.center.position, "Center");

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(Screen.width - 260, Screen.height - 255, 250, 205);
            GUI.color = Color.white;
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Camera Align Preview", "Window");
            GUILayout.Label(previewCamera.targetTexture);
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}