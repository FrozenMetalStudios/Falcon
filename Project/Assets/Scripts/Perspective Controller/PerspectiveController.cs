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
 * Filename: PerspectiveController.cs
 * 
 * Description: Allows for exchangable Camera behaviors.
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;

namespace FrozenMetal
{
namespace Perspective
{
    /// <summary>
    /// Manages the Main Camera, allowing the behavior to be switched.
    /// </summary>
    [AddComponentMenu("Perspectives/Perspective Controller")]
    [RequireComponent(typeof(Camera))]
    public class PerspectiveController : MonoBehaviour
    {
        /// <summary>
        /// The position that that camera will be following.
        /// </summary>
        public Transform target;

        /// <summary>
        /// The List of possible Cameras.
        /// </summary>
        public AbstractCamera[] cameras;

        /// <summary>
        /// The Index of the Current Camera.
        /// </summary>
        public int currentCamera = 0;

        /// <summary>
        /// The Index of the Last Camera.
        /// If the value is -1, it means no Last Camera
        /// is specified.
        /// </summary>
        public int lastCamera = -1;

        /// <summary>
        /// The progress of the current Interpolation in time.
        /// </summary>
        private float interpolationTime = 0;

        /// <summary>
        /// The maximum allowed time spent in a Camera transition.
        /// </summary>
        private float maxInterpolationTime = 0;

        /// <summary>
        /// The inverse of the maximum transition time.
        /// </summary>
        private float maxInterpolationTimeInv = 0;

        /// <summary>
        /// The reported value of the Animation Curve.
        /// </summary>
        private float lastInterpol;

        // An ease in, ease out animation curve (tangents are all flat)
        public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

        /// <summary>
        /// Sets the current Cameras Target.
        /// </summary>
        /// <param name="target">The new Target Transform</param>
        public void SetTarget(Transform target)
        {
            this.target = transform;
            cameras[currentCamera].SetTarget(target);
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
        /// Sets the Camera to use and sets a maximum transition time.
        /// </summary>
        /// <param name="newCamera">The Index of the new Camera</param>
        /// <param name="time">The maximum Transition Time in Seconds</param>
        public void SetCamera(int newCamera, float time)
        {
            // Cannot change camera to self
            if (newCamera == currentCamera)
            {
                return;
            }

            // If the last Camera was free, set the new Camera to the
            // current one and start transitioning.
            if ((time > 0) && (lastCamera == -1))
            {
                // Define the transition properties
                maxInterpolationTimeInv = 1/time;
                interpolationTime = 0;
                maxInterpolationTime = time;

                // Set the Last Camera to the Current Camera
                lastCamera = currentCamera;
            }
            else
            {
                // If interpolation is already running, then do jumpcut
                lastCamera = -1;
            }

            // Set the current Camera to the new Camera and Initialize
            currentCamera = newCamera;
            cameras[currentCamera].SetTarget(target);
            cameras[currentCamera].InitCamera();
        }

        /// <summary>
        /// Gathers the Index of the current active Camera.
        /// </summary>
        /// <returns>The int index of the current active Camera</returns>
        public int GetCurrentCamera()
        {
            return currentCamera;
        }

        /// <summary>
        /// Sets the new Camera, with a transition time of 0.
        /// </summary>
        /// <param name="newCamera">The Index of the new Camera</param>
        public void SetCamera(int newCamera)
        {
            SetCamera(newCamera, 0f);
        }

        /// <summary>
        /// Set the Camera to the first Camera in the Array.
        /// </summary>
        void Start()
        {
            // All Cameras are disabled. This is acceptable because we are only 
            // using the Camera objects as motion and rotation behaviors. The
            // Perspective Controller simply copies their behaviors.
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] != null)
                {
                    cameras[i].enabled = false;
                    cameras[i].camera.enabled = false;
                }
            }

            // Ensure the PerspectiveController's Camera is active
            this.camera.enabled = true;

            // Initialize the Current Camera and copy the properties
            cameras[currentCamera].SetTarget(target);
            cameras[currentCamera].InitCamera();

            // Get the initial Position and Rotation if the current Camera
            transform.position = cameras[currentCamera].transform.position;
            transform.rotation = cameras[currentCamera].transform.rotation;
        }

        /// <summary>
        /// Controls the Current Camera actions every Frame.
        /// </summary>
        public void LateUpdate()
        {
            // Update the Cameras Position
            UpdatePosition();

            // Update the Cameras Lookat
            UpdateLookAt();
        }

        /// <summary>
        /// Update the Current Camera Position.
        /// </summary>
        public void UpdatePosition()
        {
            // Update the Current Cameras Position
            cameras[currentCamera].UpdatePosition();

            // If the last camera hasn't been cleared, we are still in transition
            if (lastCamera != -1)
            {
                // Update our interpolation time
                interpolationTime += Time.deltaTime;

                // Update the last camera
                cameras[lastCamera].UpdatePosition();

                // Use the Animation Curve to determine where the camera should be
                lastInterpol = curve.Evaluate(interpolationTime * maxInterpolationTimeInv);
                transform.position = Vector3.Lerp(cameras[lastCamera].transform.position,
                                                  cameras[currentCamera].transform.position,
                                                  lastInterpol);

                // If we have completed our Interpolation, disable the last camera Index
                if (interpolationTime > maxInterpolationTime)
                {
                    lastCamera = -1;
                }
            }
            else
            {
                // Set the Camera Manager to the current position
                transform.position = cameras[currentCamera].transform.position;
            }
        }

        /// <summary>
        /// Controls the UpdateLookAt of the current Camera, performing smooth Transitioning
        /// if neccesary.
        /// </summary>
        public void UpdateLookAt()
        {
            // If the last camera isn't NULL, transition the Look-at Smoothly.
            if (lastCamera != -1)
            {
                // Update both Cameras Look-at
                cameras[lastCamera].UpdateLookAt();
                cameras[currentCamera].UpdateLookAt();

                // Gather both Rotational states
                Quaternion from = cameras[lastCamera].transform.rotation;
                Quaternion to = cameras[currentCamera].transform.rotation;

                // Smoothly transition to the new State
                transform.rotation = Quaternion.Slerp(from, to, lastInterpol);
            }
            else
            {
                // There was no Last Camera, so just Update the current Camera and copy the rotation
                cameras[currentCamera].UpdateLookAt();
                transform.rotation = cameras[currentCamera].transform.rotation;
            }
        }
    }
}
}