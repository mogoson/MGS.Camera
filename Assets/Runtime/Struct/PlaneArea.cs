/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  PlaneArea.cs
 *  Description  :  Define rectangle area on plane.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/8/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Cameras
{
    /// <summary>
    /// Rectangle area on plane.
    /// </summary>
    [Serializable]
    public struct PlaneArea
    {
        /// <summary>
        /// Center of area.
        /// </summary>
        public Transform center;

        /// <summary>
        /// Width of area.
        /// </summary>
        public float width;

        /// <summary>
        /// Length of area.
        /// </summary>
        public float length;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="center">Center of area.</param>
        /// <param name="width">Width of area.</param>
        /// <param name="length">Length of area.</param>
        public PlaneArea(Transform center, float width, float length)
        {
            this.center = center;
            this.width = width;
            this.length = length;
        }
    }
}