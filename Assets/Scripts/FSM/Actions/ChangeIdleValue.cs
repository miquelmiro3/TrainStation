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

			// First we disable both components to avoid the problems and warnings generated when both are enabled at the same time
			controller.GetComponent<NavMeshAgent>().enabled=false;
			controller.GetComponent<NavMeshObstacle>().enabled=false;

			// Then, we enable the components as necessary
			controller.GetComponent<NavMeshAgent>().enabled=!value;
			controller.GetComponent<NavMeshObstacle>().enabled=value;
		}
	}
}
