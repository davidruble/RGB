#pragma strict

private var leftMouseButton : int = 0;

var placement : GameObject;

function OnTriggerStay(other: Collider)
{
	Debug.Log("Colliding with something");
	
	if (other.gameObject == placement)
	{
		Debug.Log("Right placement");
		
		//actual placement
		if (Input.GetMouseButton(leftMouseButton))
		{
			transform.position = placement.transform.position;
			transform.parent = placement.transform;
			transform.rotation = placement.transform.rotation;
		}
	}
}