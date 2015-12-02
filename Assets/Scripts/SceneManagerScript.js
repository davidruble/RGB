#pragma strict
import UnityEngine.UI;

var initialRoomMusic : AudioSource;
var redRoomMusic : AudioSource;
var afterRedRoomMusic : AudioSource;
var greenRoomMusic : AudioSource;
var afterGreenRoomMusic : AudioSource;
var blueRoomMusic : AudioSource;
var afterBlueRoomMusic : AudioSource;

var maxEnemies : int = 15;

private var initialRoomIndex : int = 0;
private var redRoomIndex : int = 1;
private var afterRedRoomIndex : int = 2;
private var greenRoomIndex : int = 3;
private var afterGreenRoomIndex : int = 4;
private var blueRoomIndex : int = 5;
private var afterBlueRoomIndex : int = 6;

private var singletonReference : MySingletonClass;

private var player : GameObject;

var canvasText : Text;
var healthText : Text;
var enemiesText : Text;
private var textDisplay : boolean;
var textDisplayTimer : float = 5.0;
private var textDisplayStep : float;
private var INTERACT_STR : String = "Press 'E' to interact\nLeft mouse to attack with weapon";
private var HEALTH_STR = "Health: ";
private var ENEMIES_KO_STR = "Enemies killed: ";
private var EMPTY_STR : String = "";

function Awake()
{
    singletonReference = MySingletonClass.GetInstance();
    player = GameObject.FindWithTag("Player");
}

function Start()
{	
    if (singletonReference.initialRoom == true)
        textDisplay = true;
    
    textDisplayStep = 0.0;
    canvasText.text = EMPTY_STR;   
}

function Update () 
{
    healthText.text = HEALTH_STR + singletonReference.playerLives;
    enemiesText.text = ENEMIES_KO_STR + singletonReference.numEnemiesKilled;

    //display the gui text for a designated period of time at the start of a level
    if (textDisplay)
    {
        canvasText.text = INTERACT_STR;
        textDisplayStep += Time.deltaTime;
    }
    if (textDisplayStep > textDisplayTimer)
    {
        textDisplay = false;
        canvasText.text = EMPTY_STR;
        textDisplayStep = 0.0;
    }

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
	else if (singletonReference.redRoom == true)
	{
		if (!redRoomMusic.isPlaying)
		{
			redRoomMusic.Play();
		}
		
		//restart the level
		if(singletonReference.playerLives <= 0)
		{
		    singletonReference.playerLives = singletonReference.MAX_LIVES;
			Application.LoadLevel(redRoomIndex);
		}
		
		if(singletonReference.numEnemiesKilled >= 10)
		{
			//Debug.Log("Run! The door is unlocked!");
		}
	}
	
	//handles after red room stuff
	else if (singletonReference.afterRedRoom == true)
	{
		if (!afterRedRoomMusic.isPlaying)
		{
			afterRedRoomMusic.Play();
		}
		//print ("completed red room");
	}
	
	//handles green room stuff
	else if (singletonReference.greenRoom == true)
	{
	    if (!greenRoomMusic.isPlaying)
	    {
	        greenRoomMusic.Play();
	    }	
	}
	
	//handles after green room stuff
	else if (singletonReference.afterGreenRoom == true)
	{
	    if (!afterGreenRoomMusic.isPlaying)
	    {
	        afterGreenRoomMusic.Play();
	    }	
	}
	
	//handles blue room stuff
	else if (singletonReference.blueRoom == true)
	{
	    if (!blueRoomMusic.isPlaying)
	    {
	        blueRoomMusic.Play();
	    }	
	}
	
	//handles after blue room stuff
	else if (singletonReference.afterBlueRoom == true)
	{
	    if (!afterBlueRoomMusic.isPlaying)
	    {
	        afterBlueRoomMusic.Play();
	    }	
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
		//print("red room loaded");
		
		singletonReference.redRoom = true;
		singletonReference.initialRoom = false;
	}
	if (level == afterRedRoomIndex)
	{
		//print("after red room loaded");
		
		singletonReference.afterRedRoom = true;
		singletonReference.redRoom = false;
	}
	if (level == greenRoomIndex)
	{
		//print("green room loaded");
		
		singletonReference.greenRoom = true;
		singletonReference.afterRedRoom = false;
	}
	if (level == afterGreenRoomIndex)
	{
		//print("after green room loaded");
		
		singletonReference.afterGreenRoom = true;
		singletonReference.greenRoom = false;
	}
	if (level == blueRoomIndex)
	{
		//print("blue room loaded");
		
		singletonReference.blueRoom = true;
		singletonReference.afterGreenRoom = false;
	}
	if (level == afterBlueRoomIndex)
	{
		//print("after blue room loaded");
		
		singletonReference.afterBlueRoom = true;
		singletonReference.blueRoom = false;
	}
}