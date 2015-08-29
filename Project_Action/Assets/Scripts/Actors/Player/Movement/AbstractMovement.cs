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
 * Filename: AbstractMovement.cs
 * 
 * Description: Default Movement structure and methods.
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Actors.Player.Movement
{
    /// <summary>
    /// Describes the base requirements for a Player's movement.
    /// </summary>
    [RequireComponent (typeof(Animator))]
    [RequireComponent (typeof(Rigidbody))]
    public abstract class AbstractMovement : MonoBehaviour
    {
        /// <summary>
        /// Reference to the animator component.
        /// </summary>
        public Animator playerAnimator;

        /// <summary>
        /// Reference to the player's rigidbody.
        /// </summary>
        public Rigidbody playerRigidbody;

        /// <summary>
        /// Default Initialization
        /// </summary>
        public void InitMovement()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            playerAnimator = GetComponent<Animator>();
        }
    }
}
