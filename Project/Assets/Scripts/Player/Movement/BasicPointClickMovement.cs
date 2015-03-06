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
using Assets.Scripts.Utility.Detectors;
using Assets.Scripts.CustomEditor;

// Aliases
using VectSupp = Assets.Scripts.Utility.FmsMath.VectorMath;


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
        /// The destination coordinates.
        /// </summary>
        private Vector3 travelDestination;

        /// <summary>
        /// The vector to store the direction of the player's movement,
        /// in the X - Z plane..
        /// </summary>
        private Vector3 travelDirection;

        /// <summary>
        /// The distance that will be traveled by the movement, 
        /// in the X - Z plane.
        /// </summary>
        private float xzTotalDistance;

        /// <summary>
        /// 
        /// </summary>
        private float xzDistanceTraveled = 0;

        /// <summary>
        /// Last Position before Movement Update in Fixed Update
        /// </summary>
        private Vector3 lastPosition;

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
        /// Minimum movement Radius.
        /// </summary>
        public float minimumMovementRadius = 0.5f;

        /// <summary>
        /// Ensures that a drag move only begins after a delay.
        /// </summary>
        private PhysicsTimer dragTimer;

        /// <summary>
        /// The drag movement flag.
        /// </summary>
        [ReadOnly]
        public bool dragMovementFlag = false;

        public CapsuleCollider playerCollider;

        /// <summary>
        /// Configure the Movement on Script Load.
        /// </summary>
        void Awake()
        {
            // Initialize the Internal Components
            dragTimer = new PhysicsTimer(dragDelay);
            playerCollider = GetComponent<CapsuleCollider>();

            // Initialize the Movement parameters
            InitMovement();

            // Initialize the players position
            InitPosition();
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
        /// 
        /// </summary>
        public void InitPosition()
        {
            // Ensure the player stays along the ground
            ForceToGround();
        }

        /// <summary>
        /// In the Physics Update, look for Input and update the Player position.
        /// </summary>
        public override void Move()
        {
            // Set new Target
            DetermineState();

            // Rotate the player
            UpdateRotation();

            // Move the player around the scene.
            UpdatePosition();

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
                    // Check to see if the point is within the minimum point distance
                    if (!SphericalDetector.InGeometry(transform.position, floorHit.point, minimumMovementRadius))
                    {
                        // Store the Destination Vector
                        travelDestination = floorHit.point;

                        // Store the Travel start position
                        travelSource = transform.position;

                        // Store the Length of the Travel in the X - Z plane
                        xzTotalDistance = VectSupp.DistanceAlongPlane(travelSource, travelDestination, VectSupp.Axis.Y);

                        // Store the Direction Vector, forced in the X - Z plane.
                        travelDirection = VectSupp.DirectionAlongPlane(travelSource, travelDestination, VectSupp.Axis.Y);

                        // Init distance traveled to 0
                        xzDistanceTraveled = 0;
                        lastPosition = travelSource;

                        // Change our movement flag
                        movementState.SetMovement(MovementState.MovementFlag.Running);
                        movementState.SetRotation(MovementState.RotationFlag.Turning);
                    }
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
                    movementState.ClearMovement();
                    movementState.ClearRotation();
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
            switch (movementState.GetMovement())
            {
                case (ushort) MovementState.MovementFlag.Running:
                case (ushort) MovementState.MovementFlag.Walking:
                    // Ensure the player stays along the ground
                    ForceToGround();

                    // Determine x-z plane movement from last FixedUpdate
                    float fracDistance = 0;
                    float xzJustTraveled = VectSupp.DistanceAlongPlane(lastPosition, 
                                                                       transform.position,
                                                                       VectSupp.Axis.Y);

                    // Determine how far we've traveled overall and if we still need to move
                    xzDistanceTraveled += xzJustTraveled;
                    fracDistance = xzDistanceTraveled / xzTotalDistance;
                    if (fracDistance >= 1)
                    {
                        playerRigidbody.velocity =  Vector3.zero;
                        movementState.ClearMovement();
                        break;
                    }

                    // Predict if the speed needed is less than the players speed
                    float effectiveTravelSpeed = speed;
                    float remainingDistance = xzTotalDistance - xzDistanceTraveled;
                    float expectedTravel = speed * Time.fixedDeltaTime;
                    if (remainingDistance < expectedTravel)
                    {
                        effectiveTravelSpeed = speed / 3;
                    }
                    
                    // Add the forces for the correct movement
                    playerRigidbody.velocity = effectiveTravelSpeed * travelDirection;
                    lastPosition = transform.position;
                    break;

                default:
                    // If we are not moving, stop the object
                    playerRigidbody.velocity = Vector3.zero;
                    break;
            }
        }

        /// <summary>
        /// Update the rotation of the Player.
        /// </summary>
        public override void UpdateRotation()
        {
            // If we are rotating, update the rotation
            if (movementState.GetRotation() != 0)
            {
                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(travelDirection);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);

                // Disable any rotation
                movementState.ClearRotation();
            }
        }

        /// <summary>
        /// Implements animation statemachine.
        /// </summary>
        public override void UpdateAnimation()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void ForceToGround()
        {
            // Ensure the player stays along the ground
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, floorMask))
            {
                // Force the player to the ground
                Vector3 position = transform.position;
                position.y = hitInfo.point.y + (playerCollider.height / 2);
                transform.position = position;
            }
            else
            {
                Debug.LogError("Player Position Raycast did not hit Floor");
            }
        }
    }
}