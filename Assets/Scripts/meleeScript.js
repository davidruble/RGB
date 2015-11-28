#pragma strict

var attacking : boolean = false;		//are you currently swinging the sword?
var leftMouse : int = 0;				//variable for attack controls -- left mouse

var attackTimer : float = 0.458;		//length of attack animation
var attackTimerStep : float = 0.0;		//timer up to length of attack animation

var hitSound : AudioSource;				//sound when you hit something
var blood : Transform;					//blood effect

var zombieDeathSound : AudioSource;		//sound effect for zombie death

private var sceneManager : GameObject; 
private var singletonReference : MySingletonClass;

function Start()
{
    sceneManager = GameObject.FindWithTag("Scene Manager");
    singletonReference = MySingletonClass.GetInstance();
}

function Update () 
{
	//Debug.Log(attackTimerStep);
	
	if (Input.GetMouseButton(leftMouse))
	{
		//play the attack animation and sound effect
		GetComponent.<Animation>().Play("Take 001");
		GetComponent.<AudioSource>().Play();
		attacking = true;
	}
	
	//timer for attack duration -- prevents from killing zobies when not attacking
	if (attacking == true)
	{
		attackTimerStep += Time.deltaTime;
	}
	
	if (attackTimerStep >= attackTimer)
	{
		ResetTimer();
	}
}


//collision with an enemy
function OnCollisionEnter(collision : Collision)
{
	if (attacking == true && collision.gameObject.CompareTag("Enemy"))
	{	
		singletonReference.numEnemiesKilled++;
		
		//play blood sound and instantiate one and destroy enemy
		hitSound.Play();
		zombieDeathSound.Play();
		var bloodSplatter = Instantiate(blood, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
		Destroy(collision.gameObject);
		Destroy(bloodSplatter.gameObject, 0.5);
	}
}

//reverts variables -- stops sword from doing damage until next attack
function ResetTimer()
{
	attacking = false;
	attackTimerStep = 0.0;
}