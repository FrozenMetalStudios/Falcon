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
 * Filename: Constants.cs
 * 
 * Description: A Support Class for the Storage of Constants.
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Utility
{
    /// <summary>
    /// Utility class to store Generic static Constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Input-based Generic Static Constants.
        /// </summary>
        public static class Input
        {
            /// <summary>
            /// Represents the Left button on the mouse.
            /// </summary>
            public static int MouseLeftClick = 0;

            /// <summary>
            /// Represents the Right button on the mouse.
            /// </summary>
            public static int MouseRightClick = 1;

            /// <summary>
            /// Represents the Middle button on the mouse.
            /// </summary>
            public static int MouseMiddleClick = 2;
        }

        /// <summary>
        /// Movement-based Generic Static Constants.
        /// </summary>
        public static class Movement
        {
            /// <summary>
            /// The Layer string for Floor elements that 
            /// interact with the movement raycast.
            /// </summary>
            public static string FloorMask = @"Floor";
        }
    }
}
