/* Separate event handling class for the title screen, since it involves some timed events */

using UnityEngine;
using System.Collections;

public class spawnPlayerScene1 : MonoBehaviour {
	public GameObject prevAnim;
	public Transform PlayerPrefab; 
	public GameObject SpawnPoint;
	private bool loadedTitle; 
	private bool seen; 
	public string nextLevel; 
	
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		loadedTitle = false; 
		seen = false; 
	}
	
	// Update is called once per frame
	void Update () {
		GameObject Player = GameObject.FindWithTag ("Player"); 

		if (Player == null && 
		    prevAnim.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && 
		 	!prevAnim.GetComponent<Animator> ().IsInTransition (0)) {

			Instantiate (PlayerPrefab, new Vector2(SpawnPoint.transform.position.x,SpawnPoint.transform.position.y), Quaternion.identity);
			loadedTitle = true; 
		}

		else if (loadedTitle) {
			if (Player.GetComponent<Transform> ().lossyScale.x == 0) {
				print ("player shrunk"); 
				Application.LoadLevel (nextLevel); 
			}
			
			if (Player.GetComponent<Renderer> ().isVisible) {
				seen = true; 
			} else if (seen) {
				Destroy (Player); 
				Instantiate (PlayerPrefab, new Vector2 (SpawnPoint.transform.position.x, SpawnPoint.transform.position.y), Quaternion.identity);
			}
		}

	}
}
