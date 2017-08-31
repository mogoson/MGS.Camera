/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: AlignMark.cs
 *  Author: Mogoson   Version: 1.0   Date: 5/11/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          AlignMark               Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     5/11/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Camera
{
    using UnityEngine;

    [AddComponentMenu("Developer/Camera/AlignMark")]
    public class AlignMark : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Target of camera align to center.
        /// </summary>
        public AlignTarget alignTarget;
        #endregion

        #region Protected Method
        protected virtual void Reset()
        {
            //Reset align target.
            alignTarget = new AlignTarget(transform, new Vector2(30, 0), 5, new Range(-90, 90), new Range(1, 10));
        }
        #endregion
    }
}