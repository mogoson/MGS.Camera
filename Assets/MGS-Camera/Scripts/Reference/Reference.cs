/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Reference.cs
 *  Description  :  Define reference struct for camera control.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/8/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.CameraExtension
{
    /// <summary>
    /// Settings of mouse input.
    /// </summary>
    [Serializable]
    public struct MouseSettings
    {
        /// <summary>
        /// ID of mouse button.
        /// </summary>
        public int mouseButtonID;

        /// <summary>
        /// Sensitivity of mouse pointer.
        /// </summary>
        public float pointerSensitivity;

        /// <summary>
        /// Sensitivity of mouse ScrollWheel.
        /// </summary>
        public float wheelSensitivity;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mouseButtonID">ID of mouse button.</param>
        /// <param name="pointerSensitivity">Sensitivity of mouse pointer.</param>
        /// <param name="wheelSensitivity">Sensitivity of mouse ScrollWheel.</param>
        public MouseSettings(int mouseButtonID, float pointerSensitivity, float wheelSensitivity)
        {
            this.mouseButtonID = mouseButtonID;
            this.pointerSensitivity = pointerSensitivity;
            this.wheelSensitivity = wheelSensitivity;
        }
    }

    /// <summary>
    /// Range form min to max.
    /// </summary>
    [Serializable]
    public struct Range
    {
        /// <summary>
        /// Min value of range.
        /// </summary>
        public float min;

        /// <summary>
        /// Max value of range.
        /// </summary>
        public float max;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="min">Min value of range.</param>
        /// <param name="max">Max value of range.</param>
        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }

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

    /// <summary>
    /// Target of camera align.
    /// </summary>
    [Serializable]
    public struct AlignTarget
    {
        /// <summary>
        /// Center of align target.
        /// </summary>
        public Transform center;

        /// <summary>
        /// Angles of align.
        /// </summary>
        public Vector2 angles;

        /// <summary>
        /// Distance from camera to target center.
        /// </summary>
        public float distance;

        /// <summary>
        /// Range limit of angle.
        /// </summary>
        public Range angleRange;

        /// <summary>
        /// Range limit of distance.
        /// </summary>
        public Range distanceRange;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="center">Center of align target.</param>
        /// <param name="angles">Angles of align.</param>
        /// <param name="distance">Distance from camera to target center.</param>
        /// <param name="angleRange">Range limit of angle.</param>
        /// <param name="distanceRange">Range limit of distance.</param>
        public AlignTarget(Transform center, Vector2 angles, float distance, Range angleRange, Range distanceRange)
        {
            this.center = center;
            this.angles = angles;
            this.distance = distance;
            this.angleRange = angleRange;
            this.distanceRange = distanceRange;
        }
    }
}