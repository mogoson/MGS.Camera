/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: MouseFocus.cs
 *  Author: Mogoson   Version: 1.0   Date: 5/28/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          MouseFocus              Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     5/28/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Camera
{
    using UnityEngine;

    [RequireComponent(typeof(AroundAlignCamera))]
    [AddComponentMenu("Developer/Camera/MouseFocus")]
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
        }//Start()_end

        protected virtual void OnGUI()
        {
            //Check mouse left button double click.
            if(Event.current.isMouse && Event.current.button == 0 && Event.current.clickCount == 2)
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
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isFocus = false;
                alignCamera.AlignVeiwToTarget(defaultAlign);
            }
        }//OnGUI()_end
        #endregion
    }//class_end
}//namespace_end