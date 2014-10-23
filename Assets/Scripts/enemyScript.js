#pragma strict

private var agent : NavMeshAgent;							//agent for AI 
private var player : GameObject;							//the player object being manipulated
private var sceneManager : GameObject; 						//for access to scene manager

var movementSpeed : int = 2;								//speed of zombie movement

var playerHit : boolean = false;							//has the zombie hit the player?
var playerHitForceDuration : float = 0.2;					//duration for being flown backwards
var forceAmount : float = 12.0;								//amount of force to be applied

function Start()
{
	agent = GetComponent.<NavMeshAgent>();					//accessing the AI nav mesh
	sceneManager = GameObject.FindWithTag("Scene Manager");	//accessing the scene manager
	player = GameObject.FindWithTag("Player");				//accessing the player
}

function Update () 
{
	agent.destination = player.transform.position;	//AI moving towards player
	agent.speed = movementSpeed;					//how fast the AI moves
	
	if (playerHit)
	{
		//force the player back relative to himself when hit
		player.transform.Translate(Vector3.back * Time.deltaTime * forceAmount, Space.Self);
	}
	if (!playerHit)
	{
		//after certain time, stop flying backwards
		player.transform.Translate(Vector3.zero, Space.Self);
	}
}

//player "death" -- restarting the scene
function OnCollisionEnter(collision : Collision)
{
	//colliding with the player
	if (collision.gameObject.CompareTag("Player"))
	{
		//subtract lives from the player
		Debug.Log("Hit the player");
		sceneManager.GetComponent.<SceneManagerScript>().playerLives--;
		
		playerHit = true;
	}
}

//after collision with player has stopped
function OnCollisionExit(collision : Collision)
{
	if (collision.gameObject.CompareTag("Player"))
	{
		//this allows force to be applied for certain amount of time
		yield WaitForSeconds(playerHitForceDuration);
		
		playerHit = false;
	}
}