/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  MouseFocus.cs
 *  Description  :  Mouse button click to align camera to target.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/28/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.CameraExtension
{
    [RequireComponent(typeof(AroundAlignCamera))]
    [AddComponentMenu("Developer/CameraExtension/MouseFocus")]
    public class MouseFocus : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Layer of ray.
        /// </summary>
        public LayerMask layerMask = 1;

        /// <summary>
        /// Max distance of ray.
        /// </summary>
        public float maxDistance = 100;

        /// <summary>
        /// Current focus state.
        /// </summary>
        public bool isFocus { protected set; get; }

        /// <summary>
        /// Camera to ray.
        /// </summary>
        protected Camera targetCamera;

        /// <summary>
        /// Around and align component.
        /// </summary>
        protected AroundAlignCamera alignCamera;

        /// <summary>
        ///  Camera default align.
        /// </summary>
        protected AlignTarget defaultAlign;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            targetCamera = GetComponent<Camera>();
            alignCamera = GetComponent<AroundAlignCamera>();
        }

        protected virtual void OnGUI()
        {
            if (CheckFocusEnter())
            {
                var ray = targetCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
                {
                    var alignMark = hitInfo.transform.GetComponent<AlignMark>();
                    if (alignMark)
                    {
                        if (isFocus == false)
                        {
                            isFocus = true;
                            defaultAlign = new AlignTarget(alignCamera.target, alignCamera.currentAngles,
                                alignCamera.currentDistance, alignCamera.angleRange, alignCamera.distanceRange);
                        }
                        alignCamera.AlignVeiwToTarget(alignMark.alignTarget);
                    }
                }
            }
            else if (isFocus && CheckFocusExit())
            {
                isFocus = false;
                alignCamera.AlignVeiwToTarget(defaultAlign);
            }
        }

        /// <summary>
        /// Check enter focus state.
        /// </summary>
        /// <returns>Is enter focus state.</returns>
        protected virtual bool CheckFocusEnter()
        {
            //Mouse left button double click.
            return Event.current.isMouse && Event.current.button == 0 && Event.current.clickCount == 2;
        }

        /// <summary>
        /// Check exit focus state.
        /// </summary>
        /// <returns>Is exit focus state.</returns>
        protected virtual bool CheckFocusExit()
        {
            //Mouse right button double click.
            return Event.current.isMouse && Event.current.button == 1 && Event.current.clickCount == 2;
        }
        #endregion
    }
}