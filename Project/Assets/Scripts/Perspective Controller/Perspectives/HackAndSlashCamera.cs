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
 * Filename: HackAndSlashCamera.cs
 * 
 * Description: Implements the basic Camera movement seen in a 
 *  Top-down perspective action hack-and-slash. 
 *  
 * NOTE: IS CURRENTLY CONFIGURABLE FOR PROTOTYPING PURPOSES. CAN BE
 *  LOCKED DOWN IN THE FUTURE WHEN THE DESIRED STATE IS KNOWN.
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;
using FrozenMetal.MathUtil;

namespace FrozenMetal
{
namespace Perspective
{
    /// <summary>
    /// Provides the Perpective Topdown Camera behavior.
    /// </summary>
    [AddComponentMenu("Perspectives/Hack-and-slash Camera")]
    [RequireComponent(typeof(Camera))]
    public class HackAndSlashCamera : AbstractCamera
    {
        /// <summary>
        /// Specifies the Placement of the Camera relative to the
        /// Target in Spherical terms.
        /// </summary>
        public SphericalCoord cameraOffset;

        /// <summary>
        /// Smooths the Camera movement as it follows the player.
        /// </summary>
        public bool smoothMovementEnable = true;

        /// <summary>
        /// The speed with which the camera will be following.
        /// </summary>
        public float smoothMovementDamping = 5.0f;

        /// <summary>
        /// Specifies the Look-at offset from the Target.
        /// </summary>
        public Vector3 viewOffset = new Vector3(0, 0, 0);

        /// <summary>
        /// Specifies the Look-at rotational Offset, in Degrees.
        /// </summary>
        public Vector3 rotationOffset = new Vector3(0, 0, 0);

        /// <summary>
        /// The placement offset from the target.
        /// </summary>
        private Vector3 placementOffset;

        /// <summary>
        /// When the Scene starts, set the camera to the
        /// correct location.
        /// </summary>
        void Start()
        {
            // Call the Init to calculate the Offset
            this.InitCamera();

            // Set the Cameras Initial Position
            transform.position = target.position + placementOffset;

            // Calculate the Look-at offset from the target.
            Vector3 lookAtPosition = target.position + viewOffset;

            // Set the Cameras Rotation for the Look-at Point
            transform.rotation = Quaternion.LookRotation(lookAtPosition - transform.position) * Quaternion.Euler(rotationOffset);
        }

        /// <summary>
        /// Sets the Initial State of the Camera.
        /// </summary>
        public override void InitCamera()
        {
            // Calculate the initial offset.
            placementOffset = cameraOffset.ToCartesian();
        }

        /// <summary>
        /// Ensures the Cameras position is updated every Frame if required.
        /// </summary>
        public override void UpdatePosition()
        {
            // Update the Placement Offset if it has changed. TODO: Remove once we are satisfied with the angle.
            placementOffset = cameraOffset.ToCartesian();

            // Create a postion the Camera is heading toward for based on the offset from the target.
            Vector3 targetCamPos = target.position + placementOffset;

            // If smoothing is not enabled, just jump to the new position.
            if (smoothMovementEnable)
            {
                // Smoothly interpolate between the Camera's current position and it's target position.
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothMovementDamping * Time.deltaTime);
            }
            else
            {
                // Jump positions
                transform.position = targetCamPos;
            }
        }

        /// <summary>
        /// Updates the Cameras Lookat every Frame if required.
        /// </summary>
        public override void UpdateLookAt()
        {
            // Update the Look-at target for the Camera
            Vector3 lookAtPosition = target.position + viewOffset;
            
            // If enabled, smooth the operation
            if (smoothLookAtEnable)
            {
                // Look at and dampen the rotation
                Quaternion rotation = Quaternion.LookRotation(lookAtPosition - transform.position) * Quaternion.Euler(rotationOffset);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smoothLookAtDamping);
            }
            else
            {
                // Set the Cameras Rotation for the Look-at Point
                transform.rotation = Quaternion.LookRotation(target.position - transform.position);
            }
        }
    }
}
}