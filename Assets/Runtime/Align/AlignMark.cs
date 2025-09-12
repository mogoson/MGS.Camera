/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AlignMark.cs
 *  Description  :  Mark a gameobject for camera align to it.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/9/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  1.1
 *  Date         :  6/27/2018
 *  Description  :  Optimize display of node.
 *************************************************************************/

using UnityEngine;

namespace MGS.Cameras
{
    /// <summary>
    /// Mark gameobject for camera align to it.
    /// </summary>
    [AddComponentMenu("MGS/Camera/Align Mark")]
    public class AlignMark : MonoBehaviour
    {
        #region Field and Property
        /// <summary>
        /// Target of camera align.
        /// </summary>
        [Tooltip("Target of camera align.")]
        public AlignTarget alignTarget;
        #endregion

        #region Protected Method
        /// <summary>
        /// Reset component.
        /// </summary>
        protected virtual void Reset()
        {
            //Reset align target.
            alignTarget = new AlignTarget(transform, new Vector2(30, 0), 5, new Range(-90, 90), new Range(1, 10));
        }
        #endregion
    }
}