/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AlignCamera.cs
 *  Description  :  Camera rotate around and align to target gameobject.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Cameras
{
    /// <summary>
    /// Camera rotate around and align to target gameobject.
    /// </summary>
    [AddComponentMenu("MGS/Camera/Align Camera")]
    public class AlignCamera : AroundCamera
    {
        #region Field and Property
        /// <summary>
        /// Damper for align.
        /// </summary>
        [Tooltip("Damper for align.")]
        [Range(0, 5)]
        public float alignDamper = 2;

        /// <summary>
        /// Threshold of linear adsorbent.
        /// </summary>
        [Tooltip("Threshold of linear adsorbent.")]
        [Range(0, 1)]
        public float threshold = 0.1f;

        /// <summary>
        /// Camera is auto aligning.
        /// </summary>
        public bool IsAligning { protected set; get; }

        /// <summary>
        /// Start align event.
        /// </summary>
        public event Action OnAlignStart;

        /// <summary>
        /// End align event.
        /// </summary>
        public event Action OnAlignEnd;

        /// <summary>
        /// Last rotate angle of camera.
        /// </summary>
        protected Vector2 lastAngles;

        /// <summary>
        /// Direction record of camera.
        /// </summary>
        protected Vector3 currentDirection, targetDirection, lastDirection;

        /// <summary>
        /// Last distance frome camera to target.
        /// </summary>
        protected float lastDistance;

        /// <summary>
        /// Speed of camera move.
        /// </summary>
        protected float anglesSpeed, directionSpeed, distanceSpeed;

        /// <summary>
        /// Offset record.
        /// </summary>
        protected float anglesOffset, directionOffset, distanceOffset;

        /// <summary>
        /// MoveTowards to linear adsorbent align?
        /// </summary>
        protected bool linearAdsorbent;
        #endregion

        #region Protected Method
        /// <summary>
        /// Late update component.
        /// </summary>
        protected override void LateUpdate()
        {
            if (IsAligning)
            {
                AutoAlignView();
            }
            else
            {
                AroundByMouse();
            }
        }

        /// <summary>
        /// Auto align camera veiw to target.
        /// </summary>
        protected void AutoAlignView()
        {
            //Calculate current offset.
            var currentAnglesOffset = (targetAngles - CurrentAngles).magnitude;
            var currentDirectionOffset = (targetDirection - currentDirection).magnitude;
            var currentDistanceOffset = Mathf.Abs(targetDistance - CurrentDistance);

            //Check align finish.
            if (currentAnglesOffset < Vector3.kEpsilon && currentDirectionOffset < Vector3.kEpsilon &&
                currentDistanceOffset < Vector3.kEpsilon)
            {
                IsAligning = false;
                OnAlignEnd?.Invoke();
            }
            else
            {
                if (linearAdsorbent)
                {
                    //MoveTowards to linear adsorbent align.
                    CurrentAngles = Vector2.MoveTowards(CurrentAngles, targetAngles, anglesSpeed * Time.deltaTime);
                    currentDirection = Vector3.MoveTowards(currentDirection, targetDirection, directionSpeed * Time.deltaTime);
                    CurrentDistance = Mathf.MoveTowards(CurrentDistance, targetDistance, distanceSpeed * Time.deltaTime);
                }
                else
                {
                    //Record last.
                    lastAngles = CurrentAngles;
                    lastDirection = currentDirection;
                    lastDistance = CurrentDistance;

                    //Lerp to align.
                    CurrentAngles = Vector2.Lerp(CurrentAngles, targetAngles, alignDamper * Time.deltaTime);
                    currentDirection = Vector3.Lerp(currentDirection, targetDirection, alignDamper * Time.deltaTime);
                    CurrentDistance = Mathf.Lerp(CurrentDistance, targetDistance, alignDamper * Time.deltaTime);

                    //Check into linear adsorbent.
                    if (currentAnglesOffset / anglesOffset < threshold && currentDirectionOffset / directionOffset < threshold &&
                        currentDistanceOffset / distanceOffset < threshold)
                    {
                        anglesSpeed = (CurrentAngles - lastAngles).magnitude / Time.deltaTime;
                        directionSpeed = (currentDirection - lastDirection).magnitude / Time.deltaTime;
                        distanceSpeed = Mathf.Abs(CurrentDistance - lastDistance) / Time.deltaTime;
                        linearAdsorbent = true;
                    }
                }

                //Update position and rotation.
                transform.position = target.position + currentDirection.normalized * CurrentDistance;
                transform.rotation = Quaternion.Euler(CurrentAngles);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Align camera veiw to target.
        /// </summary>
        /// <param name="center">Around center.</param>
        /// <param name="angles">Rotate angles.</param>
        /// <param name="distance">Distance from camera to target.</param>
        public void AlignVeiwToTarget(Transform center, Vector2 angles, float distance)
        {
            target = center;
            targetAngles = angles;
            targetDistance = distance;

            //Optimal angles.
            while (targetAngles.y - CurrentAngles.y > 180)
            {
                targetAngles.y -= 360;
            }
            while (targetAngles.y - CurrentAngles.y < -180)
            {
                targetAngles.y += 360;
            }

            //Calculate lerp parameter.
            currentDirection = (transform.position - target.position).normalized;
            targetDirection = (Quaternion.Euler(targetAngles) * Vector3.back).normalized;
            CurrentDistance = Vector3.Distance(transform.position, target.position);

            //Calculate offset.
            anglesOffset = Mathf.Max((targetAngles - CurrentAngles).magnitude, Vector3.kEpsilon);
            directionOffset = Mathf.Max((targetDirection - currentDirection).magnitude, Vector3.kEpsilon);
            distanceOffset = Mathf.Max(Mathf.Abs(targetDistance - CurrentDistance), Vector3.kEpsilon);

            //Start align.
            linearAdsorbent = false;
            IsAligning = true;
            OnAlignStart?.Invoke();
        }

        /// <summary>
        /// Align camera veiw to target.
        /// </summary>
        /// <param name="alignTarget">Target of camera align.</param>
        public void AlignVeiwToTarget(AlignTarget alignTarget)
        {
            AlignVeiwToTarget(alignTarget.center, alignTarget.angles, alignTarget.distance);

            //Override range.
            angleRange = alignTarget.angleRange;
            distanceRange = alignTarget.distanceRange;
        }
        #endregion
    }
}