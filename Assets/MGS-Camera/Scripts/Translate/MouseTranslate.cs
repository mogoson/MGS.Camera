/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  MouseTranslate.cs
 *  Description  :  Mouse pointer drag to translate gameobject.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/28/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.CameraExtension
{
    [AddComponentMenu("Developer/CameraExtension/MouseTranslate")]
    public class MouseTranslate : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Target camera for translate direction.
        /// </summary>
        public Transform targetCamera;

        /// <summary>
        /// Settings of mouse button and pointer.
        /// </summary>
        public MouseSettings mouseSettings = new MouseSettings(0, 1, 0);

        /// <summary>
        /// Settings of move area.
        /// </summary>
        public PlaneArea areaSettings = new PlaneArea(null, 10, 10);

        /// <summary>
        /// Damper for move.
        /// </summary>
        [Range(0, 10)]
        public float damper = 5;

        /// <summary>
        /// Current offset base area center.
        /// </summary>
        public Vector3 currentOffset { protected set; get; }

        /// <summary>
        /// Target offset base area center.
        /// </summary>
        protected Vector3 targetOffset;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            currentOffset = targetOffset = transform.position - areaSettings.center.position;
        }

        protected virtual void Update()
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
                var mouseX = Input.GetAxis("Mouse X") * mouseSettings.pointerSensitivity;
                var mouseY = Input.GetAxis("Mouse Y") * mouseSettings.pointerSensitivity;

                //Deal with offset base direction of target camera.
                targetOffset -= targetCamera.right * mouseX;
                targetOffset -= Vector3.Cross(targetCamera.right, Vector3.up) * mouseY;

                //Range limit.
                targetOffset.x = Mathf.Clamp(targetOffset.x, -areaSettings.width, areaSettings.width);
                targetOffset.z = Mathf.Clamp(targetOffset.z, -areaSettings.length, areaSettings.length);
            }

            //Lerp and update transform position.
            currentOffset = Vector3.Lerp(currentOffset, targetOffset, damper * Time.deltaTime);
            transform.position = areaSettings.center.position + currentOffset;
        }
        #endregion
    }
}