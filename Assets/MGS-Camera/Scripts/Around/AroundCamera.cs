/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  AroundCamera.cs
 *  Description  :  Camera rotate around target gameobject.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/27/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.CameraExtension
{
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Developer/CameraExtension/AroundCamera")]
    public class AroundCamera : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Around center.
        /// </summary>
        public Transform target;

        /// <summary>
        /// Settings of mouse button, pointer and scrollwheel.
        /// </summary>
        public MouseSettings mouseSettings = new MouseSettings(1, 10, 10);

        /// <summary>
        /// Range limit of angle.
        /// </summary>
        public Range angleRange = new Range(-90, 90);

        /// <summary>
        /// Range limit of distance.
        /// </summary>
        public Range distanceRange = new Range(1, 10);

        /// <summary>
        /// Damper for move and rotate.
        /// </summary>
        [Range(0, 10)]
        public float damper = 5;

        /// <summary>
        /// Camera current angls.
        /// </summary>
        public Vector2 currentAngles { protected set; get; }

        /// <summary>
        /// Current distance from camera to target.
        /// </summary>
        public float currentDistance { protected set; get; }

        /// <summary>
        /// Camera target angls.
        /// </summary>
        protected Vector2 targetAngles;

        /// <summary>
        /// Target distance from camera to target.
        /// </summary>
        protected float targetDistance;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            currentAngles = targetAngles = transform.eulerAngles;
            currentDistance = targetDistance = Vector3.Distance(transform.position, target.position);
        }

        protected virtual void LateUpdate()
        {
            CheckMouseInput();
        }

        /// <summary>
        /// Check and deal with mouse input. 
        /// </summary>
        protected void CheckMouseInput()
        {
            if (Input.GetMouseButton(mouseSettings.mouseButtonID))
            {
                //Mouse pointer.
                targetAngles.y += Input.GetAxis("Mouse X") * mouseSettings.pointerSensitivity;
                targetAngles.x -= Input.GetAxis("Mouse Y") * mouseSettings.pointerSensitivity;

                //Range.
                targetAngles.x = Mathf.Clamp(targetAngles.x, angleRange.min, angleRange.max);
            }

            //Mouse scrollwheel.
            targetDistance -= Input.GetAxis("Mouse ScrollWheel") * mouseSettings.wheelSensitivity;
            targetDistance = Mathf.Clamp(targetDistance, distanceRange.min, distanceRange.max);

            //Lerp.
            currentAngles = Vector2.Lerp(currentAngles, targetAngles, damper * Time.deltaTime);
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, damper * Time.deltaTime);

            //Update transform position and rotation.
            transform.rotation = Quaternion.Euler(currentAngles);
            transform.position = target.position - transform.forward * currentDistance;
        }
        #endregion
    }
}