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
 * Filename: PlayerController.cs
 * 
 * Description: Summarizes and manages the player character.
 * 
 *******************************************************************/
using UnityEngine;
using Assets.Scripts.Player.Movement;

namespace Assets.Scripts.Player
{
    [RequireComponent (typeof (Rigidbody))]
    [RequireComponent (typeof (CapsuleCollider))]
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Reference to the animator component.
        /// </summary>
        // public Animator anim;

        /// <summary>
        /// Reference to the player's rigidbody.
        /// </summary>
        public Rigidbody playerRigidbody;

        /// <summary>
        /// 
        /// </summary>
        public AbstractMovement movement;


        /// <summary>
        /// 
        /// </summary>
        void Awake()
        {
            
        }


        void FixedUpdate()
        {
            // Perform the player movement
            movement.Move();
        }
    }
}