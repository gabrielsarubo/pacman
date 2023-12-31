using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationNPC : MonoBehaviour
{
	public Transform player;
	private NavMeshAgent agent;

	public GameObject[] waypoints = new GameObject[4];
	private int index = 0;
	public float maxDistance = 2;
	private bool isChasing = false;
	public float targetMaxDistance = 10;// if NPC is 10 units or less away from the PC, chase the PC

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		nearest();
	}

	private void nearest()
	{
		// create a default route for NPC
		agent.SetDestination(waypoints[index++].transform.position);
		if (index >= waypoints.Length)
		{
			index = 0;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (isChasing || Vector3.Distance(transform.position, player.position) <= targetMaxDistance)
		{
			isChasing = true;
			agent.SetDestination(player.position);
		}
		// this condition is for the NPC to follow the patrolling route
		// TODO what is the maxDistance for?
		else if (Vector3.Distance(transform.position, agent.destination) < maxDistance)
		{
			nearest();
		}

		// agent.destination = player.position; (old implementation, always chase player)
	}
}
