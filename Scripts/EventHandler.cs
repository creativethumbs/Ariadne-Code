using UnityEngine;
using System.Collections;

public class EventHandler : MonoBehaviour {
	private GameObject SpawnPoint;
	private GameObject Player; 
	public GameObject PlayerPrefab; 
	private bool seen; 

	// Use this for initialization
	void Start() {
		SpawnPoint = GameObject.FindWithTag ("SpawnPoint"); 
		SpawnPoint.GetComponent<Renderer> ().enabled = false; 

		Player = GameObject.FindWithTag ("Player"); 
		SpawnPoint.GetComponent<Transform>().localScale = Player.GetComponent<Transform>().localScale;
		SpawnPoint.GetComponent<Transform>().position = Player.GetComponent<Transform>().position;

		seen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Player != null) {
			if(Player.GetComponent<Transform>().lossyScale.x == 0) {
				print ("player shrunk"); 
			}

			if (Player.GetComponent<Renderer> ().isVisible) {
				seen = true; 
			}
		
			else if (seen) {
				Destroy (Player); 
				Player = null; 
				SpawnPoint.GetComponent<Renderer> ().enabled = true; 
			}
		}

		// only works when player is not on screen
		else if (Input.GetKeyDown (KeyCode.Return)) {
			print ("return key is held down");
			Instantiate (PlayerPrefab, new Vector2(SpawnPoint.transform.position.x,SpawnPoint.transform.position.y), Quaternion.identity);
			Player = Player = GameObject.FindWithTag ("Player"); 

			SpawnPoint.GetComponent<Renderer>().enabled = false; 
		}
	}
}
