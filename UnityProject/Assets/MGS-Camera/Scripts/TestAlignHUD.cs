/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TestAlignHUD.cs
 *  Description  :  Draw scene UI to control camera align to alignMarks.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/8/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.UCamera
{
    [AddComponentMenu("MGS/UCamera/TestAlignHUD")]
    public class TestAlignHUD : MonoBehaviour
    {
        #region Field and Property
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
            {
                AlignCameraToDefault();
            }
            if (GUILayout.Button("Align To Mark 0"))
            {
                AlignCameraToMark(alignMarks[0]);
            }
            if (GUILayout.Button("Align To Mark 1"))
            {
                AlignCameraToMark(alignMarks[1]);
            }
            if (GUILayout.Button("Align To Mark 2"))
            {
                AlignCameraToMark(alignMarks[2]);
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private void AlignCameraToMark(AlignMark alignMark)
        {
            if (isAlign == false)
            {
                isAlign = true;
                defaultAlign = new AlignTarget(alignCamera.target, alignCamera.CurrentAngles,
                alignCamera.CurrentDistance, alignCamera.angleRange, alignCamera.distanceRange);
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