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
 * Filename: PlayerAttack.cs
 * 
 * Description: Summarizes and manages the player characters skill attacks.
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	private bool skillFlag = true;
	private Animator anim;
	private float bulletSpeed = 2000;	//bullet speed for testing for bullet generation
	public Rigidbody projectile;
	private Rigidbody bullet;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> (); 
	}
	
	// Update is called once per frame
	void Update () {
		skillSelection ();

	}

	private void skillSelection(){
		//determine player skill information (skill availablitity, modifictions, animation, etc)

		//Get the players current statistics, and apply them to skills base information

		if ((Input.GetKeyDown (KeyCode.Q) && skillFlag)) {
			print("Q has been pressed");

			// deplete resource used to cast skill

			//start cooldown counter?

			// play character specific skill animation

			//this is just for testing
			bullet = (Rigidbody)Instantiate(projectile, this.transform.position, this.transform.rotation);
			bullet.AddForce(transform.forward * bulletSpeed);

			//Destroy(bullet, 3);
		}
		if ((Input.GetKeyDown (KeyCode.W) && skillFlag)) {
			print("W has been pressed");
			
			// deplete resource used to cast skill
			
			//start cooldown counter?
			
			// play character specific skill animation
		}
		if ((Input.GetKeyDown (KeyCode.E) && skillFlag)) {
			print("E has been pressed");		
			
			// deplete resource used to cast skill
			
			//start cooldown counter?
			
			// play character specific skill animation
		}
		if ((Input.GetKeyDown (KeyCode.R) && skillFlag)) {
			print("R has been pressed");		
			
			// deplete resource used to cast skill
			
			//start cooldown counter?
			
			// play character specific skill animation
		}
	}
}
