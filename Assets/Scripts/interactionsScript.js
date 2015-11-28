#pragma strict
@script RequireComponent(AudioSource)

var reachDistance : float = 3.0;	//how far the raycast goes -- for opening doors

var doorTimer : float = 0.5;		//how long is allowed for the door sound effect to play
var doorTimerCount : float = 0.0;	//counter up to doorTimer
var doorHit : boolean = false;		//has the player interacted with the door?

private var sceneManager : GameObject;
private var heldSword : GameObject;

function Start()
{
    heldSword = GameObject.FindWithTag("Equipped Sword");
    heldSword.SetActive(false);
    sceneManager = GameObject.FindWithTag("Scene Manager");
}

function Update () 
{
	//stuff for sphere cast
	var charCtrl : CharacterController = GetComponent(CharacterController);
	var p1 : Vector3 = transform.position + charCtrl.center;
	
	//timer for when player interacts with door -- alotted time for sound to play
	if (doorHit)
	{
		doorTimerCount += Time.deltaTime;
		//Debug.Log(doorTimerCount);
	}
	
	//once the timer has run out
	if (doorTimerCount >= doorTimer)
	{
		GetComponent.<AudioSource>().Stop();				
		
		sceneManager.GetComponent.<SceneManagerScript>().LoadLevel();
	}
	
	if (Input.GetKeyDown(KeyCode.E))
	{
		//if something has been hit within the raycast's limits
		var hitColliders = Physics.OverlapSphere(transform.position, 4.0);
		
		for (var i = 0; i < hitColliders.Length; i++)
		{
			//if this something was the door
			if (hitColliders[i].GetComponent.<Collider>().tag == "Door")
			{
				Debug.Log("Door is hit.");
				GetComponent.<AudioSource>().Play();
				doorHit = true;
			}
			//if this was the sword
			else if (hitColliders[i].GetComponent.<Collider>().tag == "Sword")
			{
				//"pick up" the sword and destroy the one on the ground
				Debug.Log("Picked up sword");
				heldSword.SetActive(true);
				Destroy(hitColliders[i].GetComponent.<Collider>().gameObject);
			}
			else if (hitColliders[i].GetComponent.<Collider>().tag == "Puzzle")
			{
				Debug.Log("Picked up a puzzle piece");
				hitColliders[i].transform.parent = Camera.main.transform;
				hitColliders[i].transform.position = transform.position + Vector3(0, 0, -3);
			}
		}
	}
}

