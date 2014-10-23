using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform target;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);
	}

	void Update()
	{
		agent.SetDestination (target.position);
	}
}
