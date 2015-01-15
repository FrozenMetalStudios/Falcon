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
 * Filename: AbstractCamera.cs
 * 
 * Description: Implements the basic requirements for a stand-alone 
 *  Camera behaviour.
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;

namespace FrozenMetal
{
namespace Perspective
{
    /// <summary>
    /// Default Behavior and Requirements for a Camera.
    /// </summary>
    public abstract class AbstractCamera : MonoBehaviour
    {
        /// <summary>
        /// The position that that camera will be following.
        /// </summary>
        public Transform target;

        /// <summary>
        /// Ensure the Look-at occurs smoothly.
        /// </summary>
        public bool smoothLookAtEnable = false;

        /// <summary>
        /// The damping factor of the Look-at movement.
        /// </summary>
        public float smoothLookAtDamping = 6.0f;

        /// <summary>
        /// Sets the target runtime.
        /// </summary>
        public virtual void SetTarget(Transform transform)
        {
            this.target = transform;
        }

        /// <summary>
        /// Get the Target at Runtime.
        /// </summary>
        /// <returns>The Cameras Target Transform</returns>
        public Transform GetTarget()
        {
            return target;
        }

        /// <summary>
        /// Sets the Initial State of the Camera.
        /// </summary>
        public virtual void InitCamera()
        {
            // Perform any Initialization steps required.
        }

        /// <summary>
        /// Controls the Camera actions every Frame.
        /// </summary>
        public void LateUpdate()
        {
            // Update the Cameras Position
            UpdatePosition();

            // Update the Cameras Lookat
            UpdateLookAt();
        }

        /// <summary>
        /// Ensures the Cameras position is updated every Frame if required.
        /// </summary>
        public virtual void UpdatePosition()
        {
            // Abstract Implementation does not move the Camera.
        }

        /// <summary>
        /// Updates the Cameras Lookat every Frame if required.
        /// </summary>
        public virtual void UpdateLookAt()
        {
            // If enabled, smooth the operation
            if (smoothLookAtEnable)
            {
                // Look at and dampen the rotation
                Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
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