﻿using UnityEngine;
using System;
using System.Collections;

namespace FrozenMetal
{
namespace MathUtil
{
    /// <summary>
    /// Provides a storage class for Spherical Coordinates.
    /// </summary>
    [Serializable]
    public class SphericalCoord
    {
        /// <summary>
        /// Distance from Origin to Point
        /// </summary>
        public float radius;

        /// <summary>
        /// Angle in Radians on X-Z Plane to Point from +X Axis
        /// </summary>
        public float xzAngle;

        /// <summary>
        /// Angle in Radians from +Y Axis to Point
        /// </summary>
        public float yAngle;

        /// <summary>
        /// Default Constructor for SphericalCoord.
        /// </summary>
        public SphericalCoord()
        {
            // Default values
            radius = 6f;
            xzAngle = 45f * Mathf.Deg2Rad; // Ensure store in Radians
            yAngle = 45f * Mathf.Deg2Rad;  // Ensure store in Radians
        }

        /// <summary>
        /// Full Constructor for the SphericalCoord allowing all values
        /// to be set. 
        /// </summary>
        /// <param name="r">Distance to Point from Origin</param>
        /// <param name="polar">Angle to Point on X-Z Plane from +X Axis</param>
        /// <param name="elevation">Angle from +Y Axis to Point</param>
        /// <param name="inDegrees">Default is False, meaning the constructor takes Radian values.
        ///                         True if providing angles in degrees.</param>
        public SphericalCoord(float r, float polar, float elevation, bool inDegrees = false)
        {
            // Store the values in the Object
            radius = r;
            xzAngle = polar;
            yAngle = elevation;

            // Convert from Degrees to Radians if required
            if (inDegrees)
            {
                xzAngle = xzAngle * Mathf.Deg2Rad;
                yAngle = yAngle * Mathf.Deg2Rad;
            }
        }

        /// <summary>
        /// Convert the Spherical into a Cartesian coordinates.
        /// </summary>
        /// <returns>A Vector3 representing the Cartesian coordinates</returns>
        public Vector3 ToCartesian()
        {
            return CoordSystem.Spherical2Cartesian(this);
        }
    }

    /// <summary>
    /// Provides Static Utilites for assisting in Coordinate System
    /// calculations and translations.
    /// </summary>
    public static class CoordSystem
    {
        /// <summary>
        /// Translates a given Vector3 in Cartesian coordinates to
        /// a Vector3 representing the point in Spherical coordinates.
        /// 
        /// Note: These equations and explainations assume a cordinate
        /// system where Y is up, Z is into the page and X is horizontal
        /// </summary>
        /// <param name="cartesianCoord">Vector3 in form (x,y,z)</param>
        /// <returns>
        /// SphericalCoord containing the translated point
        /// </returns>
        public static SphericalCoord Cartesian2Spherical(Vector3 cartesianCoord)
        {
            // Resulting Spherical Components
            float distance, xAngle, yAngle = 0f;

            // To ensure no division by zero, use Mathf.Epsilon approx.
            if (cartesianCoord.x == 0) cartesianCoord.x = Mathf.Epsilon;

            // Translate to new Coord System
            distance = Mathf.Sqrt((cartesianCoord.x * cartesianCoord.x)
                                + (cartesianCoord.y * cartesianCoord.y)
                                + (cartesianCoord.z * cartesianCoord.z));
            xAngle = Mathf.Atan(cartesianCoord.z / cartesianCoord.x);
            yAngle = Mathf.Acos(cartesianCoord.y / distance);

            // Ensure the X-Z plane angle is correct
            if (cartesianCoord.x < 0) xAngle += Mathf.PI;

            // Return the new Coords
            return new SphericalCoord(distance, xAngle, yAngle);
        }

        /// <summary>
        /// Translates a given Vector3 in Spherical coordinates to
        /// a Vector3 representing the point in Cartesian coordinates.
        /// 
        /// Note: These equations and explainations assume a cordinate
        /// system where Y is up, Z is into the page and X is horizontal
        /// </summary>
        /// <param name="sphericalCoord">SphericalCoord Class</param>
        /// <returns>
        /// A Vector3 containing the Cartesian representation.
        /// { distance on x axis (x),
        ///   distance on y axis (y),
        ///   distance on z axis (z) }
        /// </returns>
        public static Vector3 Spherical2Cartesian(SphericalCoord spherical)
        {
            // Resulting Rectangular Components
            float x, z, y = 0f;
            float reduction = 0f;

            // Calculate Repeated values
            reduction = spherical.radius * Mathf.Sin(spherical.yAngle);

            // Translate to final new Coords
            x = reduction * Mathf.Cos(spherical.xzAngle);
            z = reduction * Mathf.Sin(spherical.xzAngle);
            y = spherical.radius * Mathf.Cos(spherical.yAngle);

            // Return the new Coords
            return new Vector3(x, y, z); 
        }
    }
}
}
