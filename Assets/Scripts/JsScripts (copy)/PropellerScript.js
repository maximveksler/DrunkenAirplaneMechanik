#pragma strict



var moveDistance : Vector3;
var moveTime : float;

var rotationAxis : Vector3;
var rotationTime : float;


private var initialPos : Vector3;
private var target : Vector3;

private var timer : float;

//Direction of current movement, true for forward, false for backward
private var dir : boolean = true;

private var rb : Rigidbody;

var mode : int;

function Start() {
 Debug.Log("exec js file");
	mode = 0; //Rigidbody mode
	rb = gameObject.GetComponent("Rigidbody");
	if(rb==null)
		mode = 1; //Regular mode
	if(mode == 0)
		initialPos = rb.transform.position;
	else
		initialPos = transform.position;
	timer = Time.time;
	rotationAxis.Normalize();
}

function Update() {
	if(mode == 1)
	{
		if(!Mathf.Approximately(moveTime, 0.0))
		{
			target = initialPos + moveDistance;
			if(Time.time - moveTime > timer)
			{
				dir = !dir;
				timer = Time.time;
			}
		
			var timePos : float = 0.0;
		
			if(dir)
			{
				timePos = (Time.time - timer) / moveTime;
			} else {
				timePos = (timer + moveTime - Time.time) / moveTime;
			}
			
			transform.position = Vector3.Lerp(initialPos, target, timePos);
		}
		
		if(!Mathf.Approximately(rotationTime, 0.0))
		{
			var rotationMagnitude : float = 360.0 * (Time.deltaTime) / rotationTime;
			transform.rotation = Quaternion.Euler(rotationAxis.x * rotationMagnitude,
								rotationAxis.y * rotationMagnitude,
								rotationAxis.z * rotationMagnitude) * transform.rotation;
		}
	}
}

function FixedUpdate () {
	if(mode == 0)
	{
		if(!Mathf.Approximately(moveTime, 0.0))
		{
			target = initialPos + moveDistance;
			if(Time.time - moveTime > timer)
			{
				dir = !dir;
				timer = Time.time;
			}
		
			var timePos : float = 0.0;
		
			if(dir)
			{
				timePos = (Time.time - timer) / moveTime;
			} else {
				timePos = (timer + moveTime - Time.time) / moveTime;
			}
			
			rb.MovePosition(Vector3.Lerp(initialPos, target, timePos));
		}
		
		if(!Mathf.Approximately(rotationTime, 0.0))
		{
			var rotationMagnitude : float = 360.0 * (Time.deltaTime) / rotationTime;
			rb.MoveRotation(Quaternion.Euler(rotationAxis.x * rotationMagnitude,
								rotationAxis.y * rotationMagnitude,
								rotationAxis.z * rotationMagnitude) * rb.transform.rotation);
		}
	}
}

function ClampAngle (angle, min, max)
{
	if (angle < -360.0)
		angle += 360.0;
	if (angle > 360.0)
		angle -= 360.0;
	angle = Mathf.Clamp(angle, min, max);
	return angle;
}