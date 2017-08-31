/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: AlignMarkEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 5/11/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.        AlignMarkEditor           Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     5/11/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Camera
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(AlignMark), true)]
    [CanEditMultipleObjects]
    public class AlignMarkEditor : CameraEditor
    {
        #region Property and Field
        protected AlignMark script { get { return target as AlignMark; } }
        protected Camera previewCamera;
        protected RenderTexture previewTexture;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            var preview = GameObject.Find("PreviewCamera");
            if (preview)
                previewCamera = preview.GetComponent<Camera>();
            else
            {
                previewCamera = new GameObject("PreviewCamera").AddComponent<Camera>();
                previewTexture = new RenderTexture(210, 140, 16);
                previewCamera.targetTexture = previewTexture;
            }
            previewCamera.transform.parent = script.transform;
        }

        protected virtual void OnDisable()
        {
            if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<AlignMark>() == null)
                DestroyImmediate(previewCamera.gameObject, true);
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
            Handles.SphereCap(0, script.alignTarget.center.position, Quaternion.identity, nodeSize);
            DrawArrow(script.alignTarget.center.position, previewCamera.transform.position, nodeSize, "Camera", blue);
            DrawArrow(script.alignTarget.center.position, -previewCamera.transform.forward, script.alignTarget.distanceRange.min, nodeSize, "Min", blue);
            DrawArrow(script.alignTarget.center.position, -previewCamera.transform.forward, script.alignTarget.distanceRange.max, nodeSize, "Max", blue);

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            GUI.color = Color.white;
            var rect = new Rect(Screen.width - 230, Screen.height - 215, 220, 165);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Camera Align Preview", "Window");
            GUILayout.Label(previewCamera.targetTexture);
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}