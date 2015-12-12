/* Run away behavior (used in Level 2) */

using UnityEngine;
using System.Collections;

public class fleeFromPlayer : MonoBehaviour {
	private GameObject Player; 
	public float xspeed; 
	private bool moving;  

	// Use this for initialization
	void Start () {
		moving = false; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Player = GameObject.FindGameObjectWithTag ("Player"); 

		if (Player != null) {
			// runs away when player gets too close
			if (Player.transform.position.y < 1 && 
				-7 <= Player.transform.position.x && Player.transform.position.x <= -2.5) {
				moving = true;
			}

			if (moving) {
				// applying constant force to bodies
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (xspeed - GetComponent<Rigidbody2D> ().velocity.x, 0));


			}
		}
	}
}
