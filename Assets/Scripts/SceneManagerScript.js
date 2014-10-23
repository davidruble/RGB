#pragma strict

var initialRoomMusic : AudioSource;
var redRoomMusic : AudioSource;
var afterRedRoomMusic : AudioSource;

var playerLives : int = 3;			//number of lives the player has
var numEnemiesKilled : int = 0;
var maxEnemies : int = 15;

private var initialRoomIndex : int = 0;
private var redRoomIndex : int = 1;
private var afterRedRoomIndex : int = 2;
private var greenRoomIndex : int = 3;
private var afterGreenRoomIndex : int = 4;
private var blueRoomIndex : int = 5;
private var afterBlueRoomIndex : int = 6;

private var singletonReference : MySingletonClass;

var player : GameObject;

function Awake()
{
	singletonReference = MySingletonClass.GetInstance();
}

function Start()
{	
	player = GameObject.FindWithTag("Player");
}

function Update () 
{
	//check if the player went through the bottom of the map
	if (player.transform.position.y < -10)
	{
		player.transform.position.y = 20;
	}
	
	//handles initial room stuff
	if (singletonReference.initialRoom == true)
	{
		if (!initialRoomMusic.isPlaying)
		{
			initialRoomMusic.Play();
		}
	}
	
	//handles red room stuff
	if (singletonReference.redRoom == true)
	{
		if (!redRoomMusic.isPlaying)
		{
			redRoomMusic.Play();
		}
		
		Debug.Log("Num enemies killed: " + numEnemiesKilled);
		
		//restart the level
		if(playerLives <= 0)
		{
			Application.LoadLevel(redRoomIndex);
		}
		
		if(numEnemiesKilled >= 10)
		{
			Debug.Log("You won! Door should be unlocked");
		}
	}
	
	//handles after red room stuff
	if (singletonReference.afterRedRoom == true)
	{
		if (!afterRedRoomMusic.isPlaying)
		{
			afterRedRoomMusic.Play();
		}
		print ("completed red room");
	}
	
	//handles green room stuff
	if (singletonReference.greenRoom == true)
	{
		
	}
	
	//handles after green room stuff
	if (singletonReference.afterGreenRoom == true)
	{
		
	}
	
	//handles blue room stuff
	if (singletonReference.blueRoom == true)
	{
		
	}
	
	//handles after blue room stuff
	if (singletonReference.afterBlueRoom == true)
	{
		
	}
	
	
	
}

//sets conditions for the next level as the level is being loaded
function LoadLevel()
{
		if (singletonReference.initialRoom == true)
		{
			singletonReference.redRoom = true;
			singletonReference.initialRoom = false;
			Application.LoadLevel(redRoomIndex);		//load the next level
		}
		else if (singletonReference.redRoom == true)
		{
			singletonReference.afterRedRoom = true;
			singletonReference.redRoom = false;
			Application.LoadLevel(afterRedRoomIndex);		//load the next level
		}
		else if (singletonReference.afterRedRoom == true)
		{
			singletonReference.greenRoom = true;
			singletonReference.afterRedRoom = false;
			Application.LoadLevel(greenRoomIndex);
		}
		else if (singletonReference.greenRoom == true)
		{
			singletonReference.afterGreenRoom = true;
			singletonReference.greenRoom = false;
			Application.LoadLevel(afterGreenRoomIndex);
		}
		else if (singletonReference.afterGreenRoom == true)
		{
			singletonReference.blueRoom = true;
			singletonReference.afterGreenRoom = false;
			Application.LoadLevel(blueRoomIndex);
		}
		else if (singletonReference.blueRoom == true)
		{
			singletonReference.afterBlueRoom = true;
			singletonReference.blueRoom = false;
			Application.LoadLevel(afterBlueRoomIndex);
		}
}

function OnLevelWasLoaded(level : int)
{
	if (level == redRoomIndex)
	{
		print("red room loaded");
		
		singletonReference.redRoom = true;
		singletonReference.initialRoom = false;
	}
	if (level == afterRedRoomIndex)
	{
		print("after red room loaded");
		
		singletonReference.afterRedRoom = true;
		singletonReference.redRoom = false;
	}
	if (level == greenRoomIndex)
	{
		print("green room loaded");
		
		singletonReference.greenRoom = true;
		singletonReference.afterRedRoom = false;
	}
	if (level == afterGreenRoomIndex)
	{
		print("after green room loaded");
		
		singletonReference.afterGreenRoom = true;
		singletonReference.greenRoom = false;
	}
	if (level == blueRoomIndex)
	{
		print("blue room loaded");
		
		singletonReference.blueRoom = true;
		singletonReference.afterGreenRoom = false;
	}
	if (level == afterBlueRoomIndex)
	{
		print("after blue room loaded");
		
		singletonReference.afterBlueRoom = true;
		singletonReference.blueRoom = false;
	}
}