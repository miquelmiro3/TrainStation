using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NavMeshNavigator : MonoBehaviour
{

	private NavMeshAgent agent;
	private NavMeshPoints targets;

	public void GoToRandomPoint() {
		Vector3 destination = targets.GetRandomPoint();
		agent.SetDestination(destination);
	}

    // Start is called before the first frame update
    void Start()
    {
		agent=GetComponent<NavMeshAgent>();
		targets=NavMeshPointsCollection.instance.GetPoints(0);
		GoToRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
		if (agent.remainingDistance<=0.2) {
			GoToRandomPoint();
		}
    }
}
