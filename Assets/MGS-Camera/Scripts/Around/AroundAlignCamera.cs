/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  AroundAlignCamera.cs
 *  Description  :  Camera rotate around and align to target gameobject.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/27/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.CameraExtension
{
    /// <summary>
    /// Camera Align Event.
    /// </summary>
    public delegate void AlignEvent();

    [AddComponentMenu("Developer/CameraExtension/AroundAlignCamera")]
    public class AroundAlignCamera : AroundCamera
    {
        #region Property and Field
        /// <summary>
        /// Damper for align.
        /// </summary>
        [Range(0, 5)]
        public float alignDamper = 2;

        /// <summary>
        /// Threshold of linear adsorbent.
        /// </summary>
        [Range(0, 1)]
        public float threshold = 0.1f;

        /// <summary>
        /// Camera is auto aligning.
        /// </summary>
        public bool isAligning { protected set; get; }

        /// <summary>
        /// Start align event.
        /// </summary>
        public AlignEvent OnAlignStart;

        /// <summary>
        /// End align event.
        /// </summary>
        public AlignEvent OnAlignEnd;

        protected Vector2 lastAngles;
        protected Vector3 currentDirection, targetDirection, lastDirection;
        protected float lastDistance;
        protected float anglesSpeed, directionSpeed, distanceSpeed;
        protected float anglesOffset, directionOffset, distanceOffset;
        protected bool linearAdsorbent;
        #endregion

        #region Protected Method
        protected override void LateUpdate()
        {
            if (isAligning)
                AutoAlignView();
            else
                CheckMouseInput();
        }

        /// <summary>
        /// Auto align camera's veiw to target.
        /// </summary>
        protected void AutoAlignView()
        {
            //Calculate current offset.
            var currentAnglesOffset = (targetAngles - currentAngles).magnitude;
            var currentDirectionOffset = (targetDirection - currentDirection).magnitude;
            var currentDistanceOffset = Mathf.Abs(targetDistance - currentDistance);

            //Check align finish.
            if (currentAnglesOffset < Vector3.kEpsilon && currentDirectionOffset < Vector3.kEpsilon &&
                currentDistanceOffset < Vector3.kEpsilon)
            {
                isAligning = false;
                if (OnAlignEnd != null)
                    OnAlignEnd();
            }
            else
            {
                if (linearAdsorbent)
                {
                    //MoveTowards to linear adsorbent align.
                    currentAngles = Vector2.MoveTowards(currentAngles, targetAngles, anglesSpeed * Time.deltaTime);
                    currentDirection = Vector3.MoveTowards(currentDirection, targetDirection, directionSpeed * Time.deltaTime);
                    currentDistance = Mathf.MoveTowards(currentDistance, targetDistance, distanceSpeed * Time.deltaTime);
                }
                else
                {
                    //Record last.
                    lastAngles = currentAngles;
                    lastDirection = currentDirection;
                    lastDistance = currentDistance;

                    //Lerp to align.
                    currentAngles = Vector2.Lerp(currentAngles, targetAngles, alignDamper * Time.deltaTime);
                    currentDirection = Vector3.Lerp(currentDirection, targetDirection, alignDamper * Time.deltaTime);
                    currentDistance = Mathf.Lerp(currentDistance, targetDistance, alignDamper * Time.deltaTime);

                    //Check into linear adsorbent.
                    if (currentAnglesOffset / anglesOffset < threshold && currentDirectionOffset / directionOffset < threshold &&
                        currentDistanceOffset / distanceOffset < threshold)
                    {
                        anglesSpeed = (currentAngles - lastAngles).magnitude / Time.deltaTime;
                        directionSpeed = (currentDirection - lastDirection).magnitude / Time.deltaTime;
                        distanceSpeed = Mathf.Abs(currentDistance - lastDistance) / Time.deltaTime;
                        linearAdsorbent = true;
                    }
                }

                //Update position and rotation.
                transform.position = target.position + currentDirection.normalized * currentDistance;
                transform.rotation = Quaternion.Euler(currentAngles);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Align camera's veiw to target.
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
            while (targetAngles.y - currentAngles.y > 180)
                targetAngles.y -= 360;
            while (targetAngles.y - currentAngles.y < -180)
                targetAngles.y += 360;

            //Calculate lerp parameter.
            currentDistance = Vector3.Distance(transform.position, target.position);
            currentDirection = (transform.position - target.position).normalized;
            targetDirection = (Quaternion.Euler(targetAngles) * Vector3.back).normalized;

            //Calculate offset.
            anglesOffset = (targetAngles - currentAngles).magnitude;
            directionOffset = (targetDirection - currentDirection).magnitude;
            distanceOffset = Mathf.Abs(targetDistance - currentDistance);

            //Check zero value of offset.
            anglesOffset = anglesOffset > Vector3.kEpsilon ? anglesOffset : 1;
            directionOffset = directionOffset > Vector3.kEpsilon ? directionOffset : 1;
            distanceOffset = distanceOffset > Vector3.kEpsilon ? distanceOffset : 1;

            //Start align.
            linearAdsorbent = false;
            isAligning = true;
            if (OnAlignStart != null)
                OnAlignStart();
        }

        /// <summary>
        /// Align camera's veiw to target.
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