/* Line drawing code courtesy of Swati Patel */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour {
	private LineRenderer line;
	private bool isMousePressed;
	private List<Vector3> pointsList;
	public List<Rigidbody2D> colliderList;
	private Vector3 mousePos;
	public Rigidbody2D ColliderPrefab; 

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	public Transform brush; 

	// indices that keep track of where we are in the collider list
	private int startIdx;
	private int endIdx;

	public bool drawingEnabled; 

	// Structure for line points
	struct myLine
	{
		public Vector3 StartPoint;
		public Vector3 EndPoint;
	};

	void Start() {
		
		// make cursor visible (set circle to cursor)
		//Cursor.visible = false;

		drawingEnabled = true; 
		//Cursor.visible = true;

		//Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
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
		//Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		brush.transform.position = mousePosition; 

		if (drawingEnabled) {
			
			// If mouse button down, remove old line and set its color to green

			if (Input.GetButtonDown ("Fire1") || Input.GetMouseButtonDown (0)) {
				isMousePressed = true;
				line.SetVertexCount (0);
				pointsList.RemoveRange (0, pointsList.Count);
				line.SetColors (Color.black, Color.black);

				startIdx = colliderList.Count; 
			}
		
			if (Input.GetButtonUp ("Fire1") || Input.GetMouseButtonUp (0)) {
				isMousePressed = false;

				endIdx = colliderList.Count - 1; 
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

			else if (Input.GetKeyDown (KeyCode.E)) {
				var colliders = GameObject.FindGameObjectsWithTag("Collider"); 
				foreach (GameObject collider in colliders) {
					Destroy (collider); 
				}
			}
		} 
	}

	public void ToggleCollidersOff() {
		//Cursor.visible = false;

		for(int i = 0; i < colliderList.Count; i++) {
			colliderList[i].GetComponent<Renderer>().enabled = false;
		}
	}

	public void ToggleCollidersOn() {
		//Cursor.visible = true;

		for(int i = 0; i < colliderList.Count; i++) {
			colliderList[i].GetComponent<Renderer>().enabled = true;
		}
	}
}