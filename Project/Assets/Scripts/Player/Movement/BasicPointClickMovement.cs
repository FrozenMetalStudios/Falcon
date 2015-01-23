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
 * Filename: BasicPointClickMovement.cs
 * 
 * Description: Allows for point and click movement of the player.
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;
using Assets.Scripts.Utility;
using Assets.Scripts.Utility.Timers;
using Assets.Scripts.CustomEditor;

namespace Assets.Scripts.Player.Movement
{
    /// <summary>
    /// Implements basic Point and Click movement.
    /// </summary>
    public class BasicPointClickMovement : AbstractMovement
    {
        /// <summary>
        /// The Source position when a Movement occured.
        /// </summary>
        private Vector3 travelSource; 

        /// <summary>
        /// The vector to store the direction of the player's movement.
        /// </summary>
        private Vector3 travelDirection;

        /// <summary>
        /// The distance that will be travelled by the movement.
        /// </summary>
        private float travelDistance;

        /// <summary>
        /// The destination coordinates.
        /// </summary>
        private Vector3 travelDestination;

        /// <summary>
        /// The progress of the current Interpolation in time.
        /// </summary>
        private float interpolationTime = 0;

        /// <summary>
        /// A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        /// </summary>
        private int floorMask;

        /// <summary>
        /// The length of the ray from the camera into the scene.
        /// </summary>
        private float camRayLength = 100f;

        /// <summary>
        /// The delay before a Drag move is enabled.
        /// </summary>
        public float dragDelay = 0.4f;

        /// <summary>
        /// Ensures that a drag move only begins after a delay.
        /// </summary>
        private PhysicsTimer dragTimer;

        /// <summary>
        /// The drag movement flag.
        /// </summary>
        [ReadOnly]
        public bool dragMovementFlag = false;

        /// <summary>
        /// Configure the Movement on Script Load.
        /// </summary>
        void Awake()
        {
            // Initialize the Movement parameters
            InitMovement();

            // Initialize the Drag Timer
            dragTimer = new PhysicsTimer(dragDelay);
        }

        /// <summary>
        /// Initialize the Raycaster requirements.
        /// </summary>
        public override void InitMovement()
        {
            // Base Initialization
            base.InitMovement();

            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask("Floor");
        }

        /// <summary>
        /// In the Physics Update, look for Input and update the Player position.
        /// </summary>
        public override void FixedUpdate()
        {
            // Set new Target
            DetermineState();

            // Move the player around the scene.
            UpdatePosition();

            // Rotate the player
            UpdateRotation();

            // Animate the player.
            // UpdateAnimation();
        }

        /// <summary>
        /// Update the State of the Player based on Input.
        /// </summary>
        public override void DetermineState()
        {
            // If the mouse button was pressed or held
            if (Input.GetMouseButtonDown(Constants.Input.MouseLeftClick)
             || Input.GetMouseButton(Constants.Input.MouseLeftClick))
            {
                // Create a ray from the mouse cursor on screen in the direction of the camera.
                Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Create a RaycastHit variable to store information about what was hit by the ray.
                RaycastHit floorHit;

                // Update the drag timer and store the state
                dragMovementFlag = dragTimer.UpdateAndCheck();

                // Perform the raycast and if it hits something on the floor layer...
                if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
                {
                    // Store the Destination Vector
                    travelDestination = floorHit.point;

                    // Store the Direction Vector
                    travelDestination.y = 0.5f; // Currently Force to 0
                    Vector3 playerToMouse = travelDestination - transform.position;
                    travelDirection = playerToMouse.normalized;

                    // Store the Length of the Direction
                    travelDistance = Vector3.Distance(transform.position, travelDestination);

                    // Restart the Interpolation Time
                    interpolationTime = 0;

                    // Store the Travel start position
                    travelSource = transform.position;

                    // Change our movement flag
                    moving = true;
                    rotating = true;
                }
            }
            // Catch to ensure that drag movement stops in it's tracks
            else
            {
                // Clear the Mouse drag timer
                dragTimer.Reset();

                // If drag movement was occuring
                if (dragMovementFlag)
                {
                    // Stop the current motion
                    moving = false;
                    rotating = false;
                    dragMovementFlag = false;
                }
            }
        }

        /// <summary>
        /// Update the position of the Player.
        /// </summary>
        public override void UpdatePosition()
        {
            // If we are moving, update our position
            if (moving)
            {
                // Update the interpolation progress
                interpolationTime += Time.deltaTime;

                // Determine the distance traveled so far on this path
                float traveled = interpolationTime * speed;

                // Determine the fraction for the Interpolation
                float fracDistance = traveled / travelDistance;

                // Set the updated position
                playerRigidbody.MovePosition(Vector3.Lerp(travelSource, travelDestination, fracDistance));

                // If we've reached our destination, clear some variables and change the flag
                if (fracDistance >= 1)
                {
                    moving = false;
                }
            }
        }

        /// <summary>
        /// Update the rotation of the Player.
        /// </summary>
        public override void UpdateRotation()
        {
            // If we are rotating, update the rotation
            if (rotating)
            {
                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(travelDirection);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);

                // Disable any rotation
                rotating = false;
            }
        }

        /// <summary>
        /// Implements animation statemachine.
        /// </summary>
        public override void UpdateAnimation()
        {
        }
    }
}