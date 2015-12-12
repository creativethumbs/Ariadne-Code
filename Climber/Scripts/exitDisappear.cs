/* Player disappears into exit */

using UnityEngine;
using System.Collections;

public class exitDisappear : MonoBehaviour {
	private Vector3 InitialScale; 
	private bool playerFound; 

	// Use this for initialization
	void Start () {
		InitialScale = transform.localScale; 

	}
	
	// Update is called once per frame
	void Update () {
		if (!playerFound && GameObject.FindWithTag ("Player") != null) {
			playerFound = true; 

			StartCoroutine ("Disappear");
		} else if (playerFound && GameObject.FindWithTag ("Player") == null) {
			playerFound = false; 

			StopCoroutine ("Disappear");
			transform.localScale = InitialScale; 
		}
	}

	IEnumerator Disappear(){
		float progress = 0;
		Vector3 scale = new Vector3(0,0,1);
		
		while(progress <= 1){
			transform.localScale = Vector3.Lerp(InitialScale, scale, progress);
			progress += Time.deltaTime * 0.1f;
			yield return null;
		}
		transform.localScale = scale;
		
	} 
}
