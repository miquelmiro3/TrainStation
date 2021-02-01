using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetDestinationToObject", menuName = "FSM/Action/Set Destination To Object")]
public class SetDestinationToObject : FSMaction
{
	public override void Act(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		nmn.SetDestinationToObject();
	}
}
