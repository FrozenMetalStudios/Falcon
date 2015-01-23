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
 * Filename: PerspectiveCollider.cs
 * 
 * Description: Supports the changing of the Main Camera upon Target
 *  collision. 
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Perspective.Cameras;

namespace Assets.Scripts.Perspective
{
    /// <summary>
    /// Responsible for triggering a Camera change upon Camera Collision.
    /// </summary>
    [AddComponentMenu("Perspectives/Perspective Collider")]
    [RequireComponent (typeof (Collider))]
    public class PerspectiveCollider : MonoBehaviour
    {
        /// <summary>
        /// Stores a Reference to the PerspectiveController.
        /// </summary>
        public PerspectiveController perspectiveController;

        /// <summary>
        /// Index of the Camera in the PerspectiveController to set
        /// upon collision.
        /// </summary>
        public int newCamera;

        /// <summary>
        /// The transition time in Seconds to use upon collision.
        /// </summary>
        public float transitionSpeed = 3.0f;

        /// <summary>
        /// Store the old Cameras index so the process can be reverted.
        /// </summary>
        private static List<int> cameraStack = new List<int>();

        /// <summary>
        /// Ensure the Collider is configured correctly.
        /// </summary>
        void Start()
        {
            // Collider should be a Trigger
            if (!collider.isTrigger)
            {
                Debug.LogWarning("PerspectiveCollider should be set to trigger.");
            }
        }

        /// <summary>
        /// When the Camera's Target collides, switch Cameras in the PerspectiveController.
        /// </summary>
        /// <param name="other">Collider interacting with this collider</param>
        void OnTriggerEnter(Collider other)
        {
            // If the target of the Current Camera is the collider, change the Camera
            if (other.transform == perspectiveController.GetTarget())
            {
                int oldCamera = perspectiveController.currentCamera;
                cameraStack.Add(oldCamera);
                perspectiveController.SetCamera(newCamera, transitionSpeed);
            }
        }

        /// <summary>
        /// Return to the old camera upon Collider exit.
        /// </summary>
        /// <param name="other">Collider interacting with this collider</param>
        void OnTriggerExit(Collider other)
        {
            // If the target of the Current Camera is the collider, restore the old Camera
            if (other.transform == perspectiveController.GetTarget())
            {
                // If the PerspectiveController's current Camera this Colliders Camera target
                if (perspectiveController.currentCamera == newCamera)
                {
                    // Return the old camera
                    perspectiveController.SetCamera(cameraStack[cameraStack.Count - 1], transitionSpeed);
                    cameraStack.RemoveAt(cameraStack.Count-1);
                }
                else
                {
                    int lastIndex = cameraStack.LastIndexOf(newCamera);
                    if (lastIndex > -1)
                    {
                        cameraStack.RemoveAt(lastIndex);
                    }
                    else
                    {
                        Debug.LogWarning("Error in PerspectiveCollider. Cannot find camera " + newCamera + " in the camera stack");
                    }
                }
            }
        }
    }
}