/*******************************************************************
 * 
 * Copyright (C) 2015 Frozen Metal Studios - All Rights Reserved
 * 
 * NOTICE:  All information contained herein is, and remains
 * the property of Frozen Metal Studios. The intellectual and 
 * technical concepts contained herein are proprietary to 
 * Frozen Metal Studios are protected by copyright law.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from Frozen Metal Studios.
 * 
 * *****************************************************************
 * 
 * Filename: CubicalDetector.cs
 * 
 * Description: 
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Utility.Detectors
{
    /// <summary>
    /// 
    /// </summary>
    public static class CubicalDetector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="target"></param>
        /// <param name="dimensions"></param>
        /// <returns></returns>
        public static bool InGeometry(Vector3 reference, Vector3 target, Vector3 dimensions)
        {
            // Find the Distance from base point to the input
            Vector3 offset = target - reference;

            // Ensure the distance in each component don't exceed the parameters
            // The compared value is half the value of the dimension since
            // we assume the box is centered on the reference
            if ((offset.x > (dimensions.x / 2))
             || (offset.y > (dimensions.y / 2))
             || (offset.z > (dimensions.z / 2)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
