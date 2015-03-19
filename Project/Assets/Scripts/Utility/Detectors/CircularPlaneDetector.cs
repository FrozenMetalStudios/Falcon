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
 * Filename: CircularPlaneDetector.cs
 * 
 * Description: 
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;
using Assets.Scripts.Utility;

// Aliases
using VectSupp = Assets.Scripts.Utility.FmsMath.VectorMath;

namespace Assets.Scripts.Utility.Detectors
{
    /// <summary>
    /// 
    /// </summary>
    public static class CircularPlaneDetector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="target"></param>
        /// <param name="radius"></param>
        /// <param name="staticAxis"></param>
        /// <returns></returns>
        public static bool InGeometry(Vector3 reference, Vector3 target, float radius, VectSupp.Axis staticAxis = VectSupp.Axis.Y)
        {
            // Project both points to the plane
            switch (staticAxis)
            {
                case VectSupp.Axis.X:
                    reference.x = 0;
                    target.x = 0;
                    break;

                case VectSupp.Axis.Z:
                    reference.z = 0;
                    target.z = 0;
                    break;

                case VectSupp.Axis.Y:
                default:
                    reference.y = 0;
                    target.y = 0;
                    break;
            }

            // Find the Distance from base point to the input
            float distance = Vector3.Distance(reference, target);
            Logger.LogMessage(ELogCategory.Navigation, ELogLevel.Trace,
                              string.Format("Distance: {0}", distance));
            Logger.LogMessage(ELogCategory.Navigation, ELogLevel.Trace, 
                              string.Format("Radius: {0}", radius));

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