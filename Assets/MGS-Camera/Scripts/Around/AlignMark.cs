/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  AlignMark.cs
 *  Description  :  Mark a gameobject for camera to align to it.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/11/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.CameraExtension
{
    [AddComponentMenu("Developer/CameraExtension/AlignMark")]
    public class AlignMark : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Target of camera align to center.
        /// </summary>
        public AlignTarget alignTarget;
        #endregion

        #region Protected Method
        protected virtual void Reset()
        {
            //Reset align target.
            alignTarget = new AlignTarget(transform, new Vector2(30, 0), 5, new Range(-90, 90), new Range(1, 10));
        }
        #endregion
    }
}