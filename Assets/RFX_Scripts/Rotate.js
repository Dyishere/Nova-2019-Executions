
#pragma strict

var Speed : float = 3.0;  

var RotX : float = 80.0;  
var RotY : float = 80.0;  
var RotZ : float = 80.0;  

private var bottom : float;

function Awake () {
	bottom = transform.position.y;
}

//function Update () {
//	transform.Rotate(Vector3(0, Rot, 0) * Time.deltaTime, Space.World);
//}


function Update () {
	transform.Rotate(Vector3(RotX,RotY,RotZ) * Time.deltaTime, Space.Self) ;
}