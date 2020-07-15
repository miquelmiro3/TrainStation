using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNavigator : MonoBehaviour
{

	private NavMeshAgent agent;
	public NavMeshPoints targets;

    // Start is called before the first frame update
    void Start()
    {
		agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
		if (agent.remainingDistance<=0.2) {
			Vector3 destination = targets.GetRandomPoint();
			agent.SetDestination(destination);
		}
    }
}
