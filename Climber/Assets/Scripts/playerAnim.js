#pragma strict

var frames : Sprite[];
var framesPerSecond = 10;
 
function Update() {
	if(frames.length >0) {
    	var index : int = (Time.time * framesPerSecond) % frames.Length;
    	GetComponent.<SpriteRenderer>().sprite = frames[index];
    }
}