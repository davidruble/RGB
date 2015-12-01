#pragma strict
@script RequireComponent(AudioSource)

var reachDistance : float = 3.0;	//how far the raycast goes -- for opening doors

var doorTimer : float = 0.5;		//how long is allowed for the door sound effect to play
var doorTimerCount : float = 0.0;	//counter up to doorTimer
var doorHit : boolean = false;		//has the player interacted with the door?

private var sceneManager : GameObject;
private var heldSword : GameObject;
private var singletonReference : MySingletonClass;
private var charCtrl : CharacterController;

private var ceilingCol : boolean = false;   //true when there hasbeen a ceiling collision

function Start()
{
    singletonReference = MySingletonClass.GetInstance();
    heldSword = GameObject.FindWithTag("Equipped Sword");
    sceneManager = GameObject.FindWithTag("Scene Manager");
	charCtrl = GetComponent(CharacterController);
    if (singletonReference.initialRoom || singletonReference.redRoom)
        heldSword.SetActive(false);
}

function Update () 
{
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

    //prevent character from getting stuck when jumping
/*	if ((charCtrl.collisionFlags & CollisionFlags.Above) != 0)
	{
	    var hitVelocity : Vector3 = charCtrl.velocity;
	    ceilingCol = true;
	    HandleCeilingCollisions(hitVelocity);
	    //Debug.Log("Hit ceiling");
	}*/
}
/*function HandleCeilingCollisions(hitVelocity : Vector3)
{
    if (!charCtrl.isGrounded)
        charCtrl.Move(Vector3.Reflect(hitVelocity, Vector3.up) * Time.deltaTime);
    else
        ceilingCol = false;
}*/
