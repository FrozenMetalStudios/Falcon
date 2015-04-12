using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	private bool canUseSkill = true;
	private Animator anim;
	private float bulletSpeed = 2000;
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
	//need to first figure out what the player stats are, and if they have unlocked the specific ability
		/* canUseSkill = DetermineSkillUseAble(); // function from PlayerStatProfile that will look at :
		 * cooldown, availability (is it unlocked?), resourses availble to cast?
		 * playerStats();
		 **/
		//anim.SetBool ("Attack", canUseSkill); //set the blender tree Speed parameter to the meshs speed
		if ((Input.GetKeyDown (KeyCode.Q) && canUseSkill)) {
			print("Q has been pressed");
			bullet = (Rigidbody)Instantiate(projectile, this.transform.position, this.transform.rotation);
			//bullet.transform.Translate(Vector3.forward * bulletSpeed);
			//bullet.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
			bullet.AddForce(transform.forward * bulletSpeed);
			//Destroy(bullet, 3);
		}
		if ((Input.GetKeyDown (KeyCode.W) && canUseSkill)) {
			print("W has been pressed");		
		}
		if ((Input.GetKeyDown (KeyCode.E) && canUseSkill)) {
			print("E has been pressed");		
		}
		if ((Input.GetKeyDown (KeyCode.R) && canUseSkill)) {
			print("R has been pressed");		
		}
	}
}
