using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetDestinationForObjective", menuName = "FSM/Action/Set Destination For Objective")]
public class SetDestinationForObjective : FSMaction
{
	public override void Act(FSMcontroller controller) {
		TaskManager tm = controller.GetComponent<TaskManager>();
		if (tm.instant) controller.GetComponent<NavMeshNavigator>().SetDestinationToObject();
		else controller.GetComponent<NavMeshNavigator>().SetRandomDestination();
		tm.instant = false;
	}
}
