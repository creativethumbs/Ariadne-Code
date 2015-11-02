var MoveSpeed;
var MaxDist = 10;
var LookDist = 8; 
var ChaseDist = 5;
var rotationSpeed = 3; //speed of turning
var target : Transform; //the enemy's target
var myTransform : Transform;
var WayPoint : Vector2;
 
function Awake() {
    myTransform = transform; //cache transform data for easy access/preformance
}
 
function Start () {
	MoveSpeed = 3f; 
	// prevent from spinning out of control when affected by forces
	GetComponent.<Rigidbody2D>().freezeRotation = true; 
}

function Update () {
	var player : GameObject[] = GameObject.FindGameObjectsWithTag("Player"); 
	if(player.length > 0) {
		target = player[0].transform; 
		targetPos = target.transform.position;
		myPos = myTransform.position;
		var distanceFromTarget = Vector2.Distance(target.transform.position, myTransform.position);
		var direction : Vector2;
		
		// if player gets close we look
		if(distanceFromTarget <= LookDist) {
			var lookDir = targetPos - myPos;
			
			transform.LookAt(targetPos);
			transform.Rotate(new Vector2(0,-90),Space.Self);
			
			// if player gets really close we chase
   			if(distanceFromTarget <= ChaseDist) {
   				direction = (targetPos - myPos).normalized;
   				GetComponent.<Rigidbody2D>().velocity = direction*MoveSpeed; 
   			}
		}
		
		else {
			GetComponent.<Rigidbody2D>().velocity = Vector2.zero; 
		}
		
	}
}

function ChangeDirection()
{ 
   
}

function OnCollisionEnter2D(coll: Collision2D) {
}