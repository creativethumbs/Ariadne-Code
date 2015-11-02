using UnityEngine;
using System.Collections;

public class SequentialFade : MonoBehaviour {
	public GameObject prevAnim;
	float duration = 0.5f; // duration in seconds
	
	private float t = 0f; // lerp control variable

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().material.color = new Color (1f, 1f, 1f, 0F); 
	}
	
	// Update is called once per frame
	void Update () {
		if ((prevAnim.GetComponent<Animator> () == null &&
			prevAnim.GetComponent<Renderer>().material.color == Color.white) ||
			(prevAnim.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && 
		    !prevAnim.GetComponent<Animator> ().IsInTransition (0))) {

			GetComponent<Renderer>().material.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), t);

			if (t < 1){ // while t below the end limit...
				// increment it at the desired rate every update:
				t += Time.deltaTime/duration;
			}
		}
	}
}
