/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: TestAlignUI.cs
 *  Author: Mogoson   Version: 1.0   Date: 5/11/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          TestAlignUI             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     5/11/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Camera
{
    using UnityEngine;

    [AddComponentMenu("Developer/Camera/TestAlignUI")]
    public class TestAlignUI : MonoBehaviour
    {
        #region Property and Field
        public float xOffset = 10;
        public float yOffset = 10;

        public AroundAlignCamera alignCamera;
        public AlignMark[] alignMarks = new AlignMark[3];

        //Default align.
        AlignTarget defaultAlign;
        #endregion

        #region Private Method
        void Start()
        {
            defaultAlign = new AlignTarget(alignCamera.target, alignCamera.currentAngles,
                alignCamera.currentDistance, alignCamera.angleRange, alignCamera.distanceRange);
        }//Start()_end

        void OnGUI()
        {
            GUILayout.Space(yOffset);
            GUILayout.BeginHorizontal();
            GUILayout.Space(xOffset);
            GUILayout.BeginVertical();
            if (GUILayout.Button("Back Default"))
                alignCamera.AlignVeiwToTarget(defaultAlign);
            if (GUILayout.Button("Align Target0"))
                alignCamera.AlignVeiwToTarget(alignMarks[0].alignTarget);
            if (GUILayout.Button("Align Target1"))
                alignCamera.AlignVeiwToTarget(alignMarks[1].alignTarget);
            if (GUILayout.Button("Align Target2"))
                alignCamera.AlignVeiwToTarget(alignMarks[2].alignTarget);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }//OnGUI()_end
        #endregion
    }//class_end
}//namespace_end