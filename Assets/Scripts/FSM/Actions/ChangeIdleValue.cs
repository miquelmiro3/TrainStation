using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New ChangeIdleValue", menuName = "FSM/Action/Change Idle Value")]
public class ChangeIdleValue : FSMaction {

	public bool value;

	public override void Act(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		if (nmn) {
			nmn.idle=value;
			controller.GetComponent<NavMeshAgent>().enabled=!value;
		}
	}
}
