using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NavMeshNavigator : MonoBehaviour
{

	private NavMeshAgent agent;
	public NavMeshPoints targets;
	public bool destroyWhenOnDestination;

	public void SetDestination(Vector3 dest) {
		agent.SetDestination(dest);
	}

    // Start is called before the first frame update
    void Start()
    {
		agent=GetComponent<NavMeshAgent>();
		destroyWhenOnDestination=false;
    }

    // Update is called once per frame
    void Update()
    {
		if (destroyWhenOnDestination && agent.remainingDistance<=0.4) Destroy(gameObject);
		else if (agent.remainingDistance<=0.2) {
			Vector3 destination = targets.GetRandomPoint();
			agent.SetDestination(destination);
		}
    }
}
