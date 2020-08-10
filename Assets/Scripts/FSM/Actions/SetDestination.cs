using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetDestination", menuName = "FSM/Action/Set Destination")]
public class SetDestination : FSMaction
{

	public bool destroyWhenOnDestination;
	public Vector3 destination;

	public override void Act(FSMcontroller controller) {
		controller.GetComponent<NavMeshNavigator>().SetDestination(destination);
		controller.GetComponent<NavMeshNavigator>().destroyWhenOnDestination=destroyWhenOnDestination;
	}
}
