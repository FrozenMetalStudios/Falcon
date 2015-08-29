using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public Texture2D frame;
	public Rect framePosition;

	public Texture2D healthBar;
	public Rect healthBarPosition;



	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		framePosition.x = (Screen.width - framePosition.width) /2;
		GUI.DrawTexture(framePosition, frame);

	}
}
