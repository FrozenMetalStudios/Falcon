using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Slider healthSlider;   

	void Update(){


	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		GetComponent<Rigidbody>().AddForce (movement * speed*Time.deltaTime);

	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Pickup"){
			other.gameObject.SetActive(false);
		}
	}
}


