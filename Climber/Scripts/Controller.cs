/********************************************************
 * Code adapted from Nyero Faulitahs' Super Meat Boy controller for Unity
 * Controller.cs - Controls for the player
 ********************************************************/
using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{   

	public class GroundState
	{
		private GameObject player;
		private float  width;
		private float height;
		private float length;

		//GroundState constructor.  Sets offsets for raycasting.
		public GroundState(GameObject playerRef)
		{
			player = playerRef;
			width = player.GetComponent<Collider2D>().bounds.extents.x + 0.1f;
			height = player.GetComponent<Collider2D>().bounds.extents.y + 0.2f;
			length = 0.05f;
		}
		
		//Returns whether or not player is touching wall.
		public bool isWall()
		{
			RaycastHit2D hitleft = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length);
			RaycastHit2D hitright = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length);
			
			return ((hitleft.collider != null && hitleft.collider.gameObject.tag != "Sidewall") || 
				(hitright.collider != null && hitright.collider.gameObject.tag != "Sidewall")); 
		}
		
		//Returns whether or not player is touching ground.
		public bool isGround()
		{
			bool bottom1 = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y - height), -Vector2.up, length);
			bool bottom2 = Physics2D.Raycast(new Vector2(player.transform.position.x + (width - 0.2f), player.transform.position.y - height), -Vector2.up, length);
			bool bottom3 = Physics2D.Raycast(new Vector2(player.transform.position.x - (width - 0.2f), player.transform.position.y - height), -Vector2.up, length);
			if(bottom1 || bottom2 || bottom3)
				return true;
			else
				return false;
		}

		//Returns direction of wall.
		public int wallDirection()
		{
			bool left = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length);
			bool right = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length);
			
			if(left)
				return -1;
			else if(right)
				return 1;
			else
				return 0;
		}

	}


	public float    speed = 14f;
	public float    accel = 6f;
	public float airAccel = 3f;
	public float     jump = 14f;  

	public GameObject SpawnPoint;
	private Vector3 InitialScale; 

	private bool touching; 

	private GroundState groundState;
	
	void Start()
	{
		//Create an object to check if player is grounded or touching wall
		groundState = new GroundState(transform.gameObject);

		SpawnPoint = GameObject.FindWithTag ("SpawnPoint"); 
		InitialScale = transform.localScale; 
		touching = false; 
	}
	
	private Vector2 input;
	
	void Update()
	{

		//Handle input
		if (Input.GetKey (KeyCode.LeftArrow)) {
			input.x = -1;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			input.x = 1;

		} else { // not moving left or right
			input.x = 0;
		}
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			input.y = 1;
		}

		
		//Reverse player if going different direction
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, (input.x == 0) ? transform.localEulerAngles.y : (input.x + 1) * 90, transform.localEulerAngles.z);

	}
	
	void FixedUpdate()
	{
		GetComponent<Rigidbody2D>().AddForce(new Vector2(((input.x * speed) - GetComponent<Rigidbody2D>().velocity.x) * (groundState.isGround() ? accel : airAccel), 0)); //Move player.
		GetComponent<Rigidbody2D>().velocity = new Vector2((input.x == 0 && groundState.isGround()) ? 0 : GetComponent<Rigidbody2D>().velocity.x, (input.y == 1 && touching) ? jump : GetComponent<Rigidbody2D>().velocity.y); //Stop player if input.x is 0 (and grounded) and jump if input.y is 1

		if(touching && input.y == 1)
			GetComponent<Rigidbody2D>().velocity = new Vector2(-groundState.wallDirection() * speed * 0.75f, GetComponent<Rigidbody2D>().velocity.y); //Add force negative to wall direction (with speed reduction)

		input.y = 0;
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "ExitPoint") {
			print ("Inside Exit"); 
			GetComponent<Rigidbody2D>().isKinematic = true;
			StartCoroutine("Disappear");


		}

		else touching = true;

	}

	void OnCollisionStay2D(Collision2D coll){
		touching = true;
	}
	
	void OnCollisionExit2D(Collision2D coll){
		touching = false;
	}

	IEnumerator Disappear(){
		float progress = 0;
		Vector3 scale = new Vector3(0,0,1);
		
		while(progress <= 1){
			transform.localScale = Vector3.Lerp(InitialScale, scale, progress);
			progress += Time.deltaTime * 0.8f;
			yield return null;
		}
		transform.localScale = scale;
		
	} 

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "ExitPoint") {
			print ("Inside Exit"); 
			GetComponent<Rigidbody2D>().isKinematic = true;
			StartCoroutine("Disappear");


		}
	}

}


