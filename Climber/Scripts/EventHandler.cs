/* Miscellaneous event handler that takes care of exiting, spawning, level loading */

using UnityEngine;
using System.Collections;

public class EventHandler : MonoBehaviour {
	// player respawns immediately in the 2D levels
	// but for the puzzle levels the player only respawns 
	// when the 'enter' key is pressed
	public bool SpawnImmediately; 
	private GameObject SpawnPoint;
	private GameObject Player; 
	public string nextLevel; 
	public GameObject PlayerPrefab; 
	private bool seen; 

	private GameObject myDrawLine;

	// Use this for initialization
	void Start() {
		
		SpawnPoint = GameObject.FindWithTag ("SpawnPoint"); 
		myDrawLine = GameObject.Find ("ScriptPrefab"); 

		// can't be dereferencing any null pointers
		if(SpawnPoint != null) 
			SpawnPoint.GetComponent<Renderer> ().enabled = !SpawnImmediately; 

		Player = GameObject.FindWithTag ("Player"); 

		if (Player != null) {
			SpawnPoint.GetComponent<Transform> ().localScale = Player.GetComponent<Transform> ().localScale;
			SpawnPoint.GetComponent<Transform> ().position = Player.GetComponent<Transform> ().position;
		}

		seen = false;
	}
	
	// Update is called once per frame
	void Update () {
		Player = GameObject.FindWithTag ("Player"); 
		GameObject ExitPoint = GameObject.FindWithTag ("ExitPoint"); 

		if (Player != null) {
			if (Player.GetComponent<Transform> ().lossyScale.x == 0) {
				print ("player shrunk"); 
				Application.LoadLevel (nextLevel); 
			}
			if (ExitPoint.GetComponent<Transform> ().lossyScale.x == 0) {
				ResetPlayer(); 
			}

			if (Player.GetComponent<Renderer> ().isVisible) {
				seen = true; 
			} else if (seen) {
				ResetPlayer(); 
				/*
				Destroy (Player); 

				if (!SpawnImmediately) {
					SpawnPoint.GetComponent<Renderer> ().enabled = true; 

					myDrawLine.GetComponent<DrawLine>().ToggleCollidersOn(); 
					myDrawLine.GetComponent<DrawLine>().drawingEnabled = true;
					myDrawLine.GetComponent<DrawLine>().brush.GetComponent<Renderer>().enabled = true;
				}
				*/
			}
		}

		// only works when player is not on screen, because Player is only null 
		// when it is destroyed
		else {
			// spawn player when return is pressed
			if (!SpawnImmediately && Input.GetKeyDown (KeyCode.Return)) {
				print ("return key is held down");
				Instantiate (PlayerPrefab, new Vector2 (SpawnPoint.transform.position.x, SpawnPoint.transform.position.y), Quaternion.identity);

				SpawnPoint.GetComponent<Renderer> ().enabled = false; 

				myDrawLine.GetComponent<DrawLine>().ToggleCollidersOff(); 
				myDrawLine.GetComponent<DrawLine>().drawingEnabled = false;
				myDrawLine.GetComponent<DrawLine>().brush.GetComponent<Renderer>().enabled = false;
				//Cursor.visible = false;

			} else if(SpawnImmediately) {
				Instantiate (PlayerPrefab, new Vector2 (SpawnPoint.transform.position.x, SpawnPoint.transform.position.y), Quaternion.identity);
			}
		}
	}

	void ResetPlayer() {
		Destroy (Player); 
		
		if (!SpawnImmediately) {
			SpawnPoint.GetComponent<Renderer> ().enabled = true; 
			
			myDrawLine.GetComponent<DrawLine>().ToggleCollidersOn(); 
			myDrawLine.GetComponent<DrawLine>().drawingEnabled = true;
			myDrawLine.GetComponent<DrawLine>().brush.GetComponent<Renderer>().enabled = true;
			//Cursor.visible = true;
		}
	}
}
