/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  TestAlignUI.cs
 *  Description  :  Draw scene UI to control camera align to alignMarks.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/11/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.CameraExtension
{
    [AddComponentMenu("Developer/CameraExtension/TestAlignUI")]
    public class TestAlignUI : MonoBehaviour
    {
        #region Property and Field
        public float xOffset = 10;
        public float yOffset = 10;

        public AroundAlignCamera alignCamera;
        public AlignMark[] alignMarks = new AlignMark[3];

        private AlignTarget defaultAlign;
        private bool isAlign = false;
        #endregion

        #region Private Method
        private void OnGUI()
        {
            GUILayout.Space(yOffset);
            GUILayout.BeginHorizontal();
            GUILayout.Space(xOffset);
            GUILayout.BeginVertical();
            if (GUILayout.Button("Back To Default"))
                AlignCameraToDefault();
            if (GUILayout.Button("Align To Mark 0"))
                AlignCameraToMark(alignMarks[0]);
            if (GUILayout.Button("Align To Mark 1"))
                AlignCameraToMark(alignMarks[1]);
            if (GUILayout.Button("Align To Mark 2"))
                AlignCameraToMark(alignMarks[2]);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private void AlignCameraToMark(AlignMark alignMark)
        {
            if (isAlign == false)
            {
                isAlign = true;
                defaultAlign = new AlignTarget(alignCamera.target, alignCamera.currentAngles,
                alignCamera.currentDistance, alignCamera.angleRange, alignCamera.distanceRange);
            }
            alignCamera.AlignVeiwToTarget(alignMark.alignTarget);
        }

        private void AlignCameraToDefault()
        {
            if (isAlign)
            {
                isAlign = false;
                alignCamera.AlignVeiwToTarget(defaultAlign);
            }
        }
        #endregion
    }
}