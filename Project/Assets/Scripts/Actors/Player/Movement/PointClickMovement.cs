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


namespace Assets.Scripts.Actors.Player.Movement
{
    /// <summary>
    /// Implements Point and Click movement.
    /// </summary>
    [RequireComponent (typeof (NavMeshAgent))]
    public class PointClickMovement : AbstractMovement
    {
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
        [ReadOnly] public bool dragMovementFlag = false;

        /// <summary>
        /// The Minimum movement distance.
        /// </summary>
        public float minimumMovementRadius = 0.5f;

        /// <summary>
        /// Reference to the Players NavMeshAgent.
        /// </summary>
        public NavMeshAgent navMeshAgent;

        /// <summary>
        /// 
        /// </summary>
        private Vector3 initialRotation;

        /// <summary>
        /// 
        /// </summary>
        [ReadOnly] public Vector3 nonNavigableDestination;

        /// <summary>
        /// Configure before first Frame load.
        /// </summary>
        void Start()
        {
            // Base Initialization
            base.InitMovement();

            // Initialize the Internal Components
            dragTimer = new PhysicsTimer(dragDelay);
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updatePosition = false; // Allows Animation Sync
            navMeshAgent.updateRotation = false; // Allows rotation offset incase of bad model import
            navMeshAgent.acceleration = float.MaxValue; // Set infinitly high acceleration
            navMeshAgent.angularSpeed = float.MaxValue; // Set infinitly high angular speed
            navMeshAgent.stoppingDistance = 0.0f; // Ensures the player is as close to the location as possible
            navMeshAgent.autoBraking = true; // Forces Autobraking, which ensures no overshoot

            // Initialize Animation Components
            playerAnimator = GetComponent<Animator>(); 

            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask("Floor");

            // Store initial rotation
            initialRotation = transform.rotation.eulerAngles;
        }

        /// <summary>
        /// Perform the Movement.
        /// </summary>
        public void Move()
        {
            // Find out where we want to be based on input
            GetDestination();

            // TODO: Something to affect movement speed?

            // Determine where we will be next and how fast
            // DetermineAnimationParameters();

            // Update the Rotation
            UpdateRotation();

            // Update the Position
            UpdatePosition();
        }

        /// <summary>
        /// Get the Destination based on Input.
        /// </summary>
        public void GetDestination()
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
                    GameObject objectHit;
                    objectHit = floorHit.collider.gameObject;

                    // Check to see if the point is within the minimum point distance
                    if (!CircularPlaneDetector.InGeometry(transform.position, floorHit.point, minimumMovementRadius, VectSupp.Axis.Y)
                     && ("Floor" == objectHit.gameObject.tag))
                    {
                        navMeshAgent.SetDestination(floorHit.point);

                        if ("Floor" == objectHit.gameObject.tag)
                        {
                            nonNavigableDestination = Vector3.zero;
                        }
                        else
                        {
                            nonNavigableDestination = floorHit.point;
                        }
                    }
                    else
                    {
                        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                        UpdateRotation(floorHit.point);
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
                    dragMovementFlag = false;
                }
            }
        }

        /// <summary>
        /// Update the position of the Player.
        /// </summary>
        public void DetermineAnimationParameters()
        {
            // Update animation state and velocity info?
            // http://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html

            //Get the velocity of the player mesh
            float speed = navMeshAgent.velocity.magnitude;

            playerAnimator.SetFloat("Speed", speed); //Set the blender tree Speed parameter to the meshs speed

        }

        void UpdateRotation()
        {
            if (0.0 != navMeshAgent.remainingDistance)
            {
                UpdateRotation(navMeshAgent.steeringTarget);
            }
            else
            {
                if (Vector3.zero != nonNavigableDestination)
                {
                    UpdateRotation(nonNavigableDestination);
                }
            }
        }

        void UpdateRotation(Vector3 point)
        {
            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(VectSupp.DirectionAlongPlane(transform.position, point));
            newRotation = newRotation * Quaternion.Euler(initialRotation);

            // Set the player's rotation to this new rotation.
            this.transform.rotation = newRotation;
        }

        /// <summary>
        /// Move the Player.
        /// </summary>
        void UpdatePosition()
        {
            Logger.LogMessage(eLogCategory.Navigation,
                              eLogLevel.Trace,
                              string.Format("Destination  : {0}", navMeshAgent.destination));
            Logger.LogMessage(eLogCategory.Navigation,
                              eLogLevel.Trace,
                              string.Format("Next Position: {0}", navMeshAgent.nextPosition));

            // Update the Position of the Character
            transform.position = navMeshAgent.nextPosition;
        }
    }
}