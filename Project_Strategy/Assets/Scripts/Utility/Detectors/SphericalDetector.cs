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
 * Filename: SphericalDetector.cs
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
    public static class SphericalDetector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="target"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static bool InGeometry(Vector3 reference, Vector3 target, float radius)
        {
            // Find the Distance from base point to the input
            float distance = Vector3.Distance(reference, target);
            Debug.Log(string.Format("Distance: {0}", distance));
            Debug.Log(string.Format("Radius: {0}", radius));

            // If the distance from our center to the 
            if (distance <= radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
