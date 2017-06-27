/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: AbstractData.cs
 *  Author: Mogoson   Version: 1.0   Date: 4/29/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         AbstractData             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     4/29/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Camera
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct MouseSettings
    {/// <summary>
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

        public MouseSettings(int mouseButtonID, float pointerSensitivity, float wheelSensitivity)
        {
            this.mouseButtonID = mouseButtonID;
            this.pointerSensitivity = pointerSensitivity;
            this.wheelSensitivity = wheelSensitivity;
        }//M...()_end
    }//MouseSettings3_end

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

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }//Range()_end
    }//Range_end

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

        public PlaneArea(Transform center, float width, float length)
        {
            this.center = center;
            this.width = width;
            this.length = length;
        }//PlaneArea()_end
    }//PlaneArea_end

    [Serializable]
    public struct AlignTarget
    {
        /// <summary>
        /// Center of align and around.
        /// </summary>
        public Transform center;

        /// <summary>
        /// Angles of camera align.
        /// </summary>
        public Vector2 angles;

        /// <summary>
        /// Distance from camera to target of align.
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

        public AlignTarget(Transform center, Vector2 angles, float distance, Range angleRange, Range distanceRange)
        {
            this.center = center;
            this.angles = angles;
            this.distance = distance;
            this.angleRange = angleRange;
            this.distanceRange = distanceRange;
        }//A...()_end
    }//AlignTarget_end
}//namespace_end