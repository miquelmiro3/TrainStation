using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetDestinationForObjective", menuName = "FSM/Action/Set Destination For Objective")]
public class SetDestinationForObjective : FSMaction
{
	public override void Act(FSMcontroller controller) {
		controller.GetComponent<NavMeshNavigator>().SetDestinationToObject();
	}
}
