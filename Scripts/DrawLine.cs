/* Line drawing code courtesy of Swati Patel */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour {
	private LineRenderer line;
	private bool isMousePressed;
	public List<Vector3> pointsList;
	private List<Rigidbody2D> colliderList;
	private Vector3 mousePos;
	public Rigidbody2D ColliderPrefab; 
	//public Transform PlayerPrefab; 
	//public GameObject SpawnPoint;
	public Transform brush; 

	private int startIdx;
	private int endIdx;
	
	// Structure for line points
	struct myLine
	{
		public Vector3 StartPoint;
		public Vector3 EndPoint;
	};

	void Start() {
		Cursor.visible = false;
	}

	//	-----------------------------------	
	void Awake () {
		// Create line renderer component and set its property
		line = gameObject.AddComponent<LineRenderer> ();
		line.material = new Material (Shader.Find ("Particles/Additive"));
		line.SetVertexCount (0);
		line.SetWidth (0.1f, 0.1f);
		line.SetColors (Color.black, Color.black);
		line.useWorldSpace = true;	
		isMousePressed = false;
		pointsList = new List<Vector3> ();
		colliderList = new List<Rigidbody2D> ();

	}
	//	-----------------------------------	
	void Update () {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		brush.transform.position = mousePosition; 

		if (Input.GetKeyDown (KeyCode.H)) {
			print ("h key is held down");
			ToggleColliders(); 
		}

		// If mouse button down, remove old line and set its color to green
		if (Input.GetMouseButtonDown (0)) {
			isMousePressed = true;
			line.SetVertexCount (0);
			pointsList.RemoveRange (0, pointsList.Count);
			line.SetColors (Color.black, Color.black);
		}
		
		if (Input.GetMouseButtonUp (0)) {
			isMousePressed = false;
		}

		// user is drawing
		if (isMousePressed) {
			mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos.z = 0;
			if (!pointsList.Contains (mousePos)) {
				pointsList.Add (mousePos);
				line.SetVertexCount (pointsList.Count);
				line.SetPosition (pointsList.Count - 1, (Vector3)pointsList [pointsList.Count - 1]);

				if (pointsList.Count > 1) {
					const float ColliderWidth = 0.1f;
					Vector3 point1 = pointsList [pointsList.Count - 2];
					Vector3 point2 = pointsList [pointsList.Count - 1];

					Rigidbody2D obj;
					obj = Instantiate (ColliderPrefab, transform.position, transform.rotation) as Rigidbody2D;

					obj.transform.position = (point1 + point2) / 2;
					obj.transform.right = (point2 - point1).normalized;

					obj.transform.parent = this.transform;

					colliderList.Add (obj); 
				}

			}
		} 
	}

	void ToggleColliders() {
		for(int i = 0; i < colliderList.Count; i++) {
			colliderList[i].GetComponent<Renderer>().enabled = !colliderList[i].GetComponent<Renderer>().enabled;
		}
	}
}