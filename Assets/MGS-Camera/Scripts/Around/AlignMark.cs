/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AlignMark.cs
 *  Description  :  Mark a gameobject for camera align to it.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.CameraExtension
{
    /// <summary>
    /// Mark a gameobject for camera align to it.
    /// </summary>
    [AddComponentMenu("Mogoson/CameraExtension/AlignMark")]
    public class AlignMark : MonoBehaviour
    {
        #region Field and Property
        /// <summary>
        /// Target of camera align.
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