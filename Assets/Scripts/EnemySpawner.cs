using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public List<GameObject> enemySpawners;
	public GameObject enemy;

	private bool spawned = false;

	void OnTriggerEnter(Collider collider)
	{
		if (!spawned) 
		{
			SpawnEnemies ();
		}
	}

	void SpawnEnemies()
	{
		if (enemySpawners == null)
		{
			return;
		}

		foreach (GameObject spawner in enemySpawners)
		{
			GameObject.Instantiate(enemy, spawner.transform.position, spawner.transform.rotation);
			spawned = true;
		}
	}
}
