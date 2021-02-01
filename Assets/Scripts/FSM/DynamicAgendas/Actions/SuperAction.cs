using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SuperAction", menuName = "FSM/Action/SuperAction")]
public class SuperAction : FSMaction {

	public FSMaction[] actions;

	public override void Act(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		if (nmn && nmn.destinationReached && !nmn.working) { // he vist que el condition no s'utilitza almenys no ho he trobat
			foreach (FSMaction action in actions) {
				action.Act(controller);
			}
		}
	}
}
