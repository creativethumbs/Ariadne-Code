using UnityEngine;
using System.Collections;

public class spawnPlayerScene1 : MonoBehaviour {
	public GameObject prevAnim;
	public Transform PlayerPrefab; 
	public GameObject SpawnPoint;
	
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag("Player") == null && 
		    prevAnim.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && 
		 	!prevAnim.GetComponent<Animator> ().IsInTransition (0)) {

			Instantiate (PlayerPrefab, new Vector2(SpawnPoint.transform.position.x,SpawnPoint.transform.position.y), Quaternion.identity);

		}

	}
}
