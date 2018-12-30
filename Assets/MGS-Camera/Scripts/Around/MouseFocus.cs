/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MouseFocus.cs
 *  Description  :  Mouse click to align camera to target.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.UCamera
{
    /// <summary>
    /// Mouse click to focus camera to target.
    /// </summary>
    [AddComponentMenu("Mogoson/UCamera/MouseFocus")]
    [RequireComponent(typeof(AroundAlignCamera))]
    public class MouseFocus : MonoBehaviour
    {
        #region Field and Property
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
        public bool IsFocus { protected set; get; }

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
            Initialize();
        }

        protected virtual void OnGUI()
        {
            if (CheckFocusEnter())
            {
                var ray = targetCamera.ScreenPointToRay(Input.mousePosition);
                var hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
                {
                    var alignMark = hitInfo.transform.GetComponent<AlignMark>();
                    if (alignMark)
                    {
                        if (IsFocus == false)
                        {
                            defaultAlign = new AlignTarget(alignCamera.target, alignCamera.CurrentAngles,
                                alignCamera.CurrentDistance, alignCamera.angleRange, alignCamera.distanceRange);
                            IsFocus = true;
                        }
                        alignCamera.AlignVeiwToTarget(alignMark.alignTarget);
                    }
                }
            }
            else if (IsFocus && CheckFocusExit())
            {
                alignCamera.AlignVeiwToTarget(defaultAlign);
                IsFocus = false;
            }
        }

        /// <summary>
        /// Initialize component.
        /// </summary>
        protected virtual void Initialize()
        {
            targetCamera = GetComponent<Camera>();
            alignCamera = GetComponent<AroundAlignCamera>();
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