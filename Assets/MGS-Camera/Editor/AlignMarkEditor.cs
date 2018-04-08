/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AlignMarkEditor.cs
 *  Description  :  Custom editor for AlignMarkEditor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/11/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.CameraExtension
{
    [CustomEditor(typeof(AlignMark), true)]
    [CanEditMultipleObjects]
    public class AlignMarkEditor : CameraEditor
    {
        #region Field and Property
        protected AlignMark script { get { return target as AlignMark; } }
        protected const string previewCameraName = "PreviewCamera";
        protected Camera previewCamera;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            var preview = GameObject.Find(previewCameraName);
            if (preview)
                previewCamera = preview.GetComponent<Camera>();
            else
            {
                var previewTexture = new RenderTexture(240, 180, 16);
                previewCamera = new GameObject(previewCameraName).AddComponent<Camera>();
                previewCamera.targetTexture = previewTexture;
            }
            previewCamera.transform.parent = script.transform;
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
            if (script.alignTarget.center == null)
                return;

            DrawPositionHandle(script.alignTarget.center);

            previewCamera.transform.rotation = Quaternion.Euler(script.alignTarget.angles);
            previewCamera.transform.position = script.alignTarget.center.position + previewCamera.transform.rotation * Vector3.back * script.alignTarget.distance;

            GUI.color = Handles.color = blue;
            Handles.Label(script.alignTarget.center.position, "Center");
            DrawSphereCap(script.alignTarget.center.position, Quaternion.identity, nodeSize);
            DrawArrow(script.alignTarget.center.position, previewCamera.transform.position, nodeSize, "Camera", blue);
            DrawArrow(script.alignTarget.center.position, -previewCamera.transform.forward, script.alignTarget.distanceRange.min, nodeSize, "Min", blue);
            DrawArrow(script.alignTarget.center.position, -previewCamera.transform.forward, script.alignTarget.distanceRange.max, nodeSize, "Max", blue);

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            GUI.color = Color.white;
            var rect = new Rect(Screen.width - 260, Screen.height - 255, 250, 205);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Camera Align Preview", "Window");
            GUILayout.Label(previewCamera.targetTexture);
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}