/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: AroundCameraEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 4/28/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       AroundCameraEditor         Ignore.
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
    using UnityEditor.SceneManagement;
    using UnityEngine;

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
        }//OnEnable()_end

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
            Handles.SphereCap(0, script.target.position, Quaternion.identity, nodeSize);
            Handles.Label(script.target.position, "Target");
            var direction = (script.transform.position - script.target.position).normalized;
            DrawArrow(script.target.position, script.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(script.target.position, direction, script.distanceRange.min, nodeSize, "Min", blue);
            DrawArrow(script.target.position, direction, script.distanceRange.max, nodeSize, "Max", blue);

            DrawSceneTool();
        }//OnSceneGUI()_end

        protected virtual void DrawSceneTool()
        {
            GUI.color = Color.white;
            var rect = new Rect(10, Screen.height - 110, 170, 60);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Current Around", "Window");
            GUILayout.BeginHorizontal();
            GUILayout.Label("Angles:");
            EditorGUI.BeginChangeCheck();
            if(Application.isPlaying)
            {
                GUILayout.Label(script.currentAngles.x.ToString("F2"));
                GUILayout.Label(script.currentAngles.y.ToString("F2"));
            }
            else
            {
                angles.x = float.Parse(GUILayout.TextField(angles.x.ToString("F2")));
                angles.y = float.Parse(GUILayout.TextField(angles.y.ToString("F2")));
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Distance:");
            if (Application.isPlaying)
                GUILayout.Label(script.currentDistance.ToString("F2"));
            else
                distance = float.Parse(GUILayout.TextField(distance.ToString("F2")));
            if (EditorGUI.EndChangeCheck())
                EditorSceneManager.MarkAllScenesDirty();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            Handles.EndGUI();
        }//DrawSceneTool()_end
        #endregion
    }//class_end
}//namespace_end