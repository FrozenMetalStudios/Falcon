﻿/*******************************************************************
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
    public class PlayerController : MonoBehaviour
    {
        public AbstractMovement movement;

        /*
        void Start()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }


        void Spawn()
        {
            // If the player has no health left...
            //if(playerHealth.currentHealth <= 0f)
            //{
            //    // ... exit the function.
            //    return;
            //}

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        */
    }
}