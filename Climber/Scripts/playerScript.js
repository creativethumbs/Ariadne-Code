#pragma strict

private var velocity : Vector3; //Our movement velocity
private var speed : float; 
private var movex : float;
private var movey : float;
//direction of movement
private var prevDir; 
public var lookSpeed : float;
private var curLoc : Vector3;
private var prevLoc : Vector3;
private var enteredSpace : boolean;

function Start() {
	velocity = this.transform.forward;
	speed = 4.0; 
	movex = 0f; 
	movey = 0f; 
	prevDir = false;
	lookSpeed = 10;
	enteredSpace = false;
}

function Update () {
    InputListen();
    
    var moveDirection = gameObject.transform.position - prevLoc;
    
    if (moveDirection != Vector3.zero) {
	     var angle = Mathf.Atan2(-moveDirection.y, -moveDirection.x) * Mathf.Rad2Deg;
	     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}

function InputListen() {
	 prevLoc = curLoc;
	 curLoc = transform.position;
	 
	 if(Input.GetKey(KeyCode.LeftArrow))
	     curLoc.x -= speed * Time.fixedDeltaTime;
	 if(Input.GetKey(KeyCode.RightArrow))
	     curLoc.x += speed * Time.fixedDeltaTime;
	 if(Input.GetKey(KeyCode.UpArrow))
	     curLoc.y += speed * Time.fixedDeltaTime;
	 if(Input.GetKey(KeyCode.DownArrow))
	     curLoc.y -= speed * Time.fixedDeltaTime;
	 
	 transform.position = curLoc;
     
}

