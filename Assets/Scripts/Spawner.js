#pragma strict

private var zombiesSpawned : int = 0; 	//zombie counter
private var waitTime : float;			//time to wait in between spawns

var zombiePrefab : GameObject; 	//prefab to spawn
var spawn : boolean = true; 	//is a zombie being spawned or not?
var player : GameObject;		//stores the player for player interaction
var minWait : float = 1.0; 		//min amount for wait time range
var maxWait : float = 10.0;		//max amount for wait time range
var maxZombies : int = 10;		//max number of zombies to be spawned

function Start()
{
	waitTime = Random.Range(minWait, maxWait);
	spawn = true;
}

function Update () 
{
	if (spawn && zombiesSpawned < maxZombies)
	{
		Spawn();
	}
}

function Spawn()
{
	Instantiate(zombiePrefab, transform.position, transform.rotation); //spawn at spawner location
	zombiesSpawned += 1;
	
	NewWaitTime();
	
	SetSpawn();
}

function SetSpawn()
{
	yield WaitForSeconds(waitTime);
	spawn = true;
}

function NewWaitTime()
{
	waitTime = Random.Range(minWait, maxWait);
	spawn = false;
}