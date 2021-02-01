using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ObjectiveFound", menuName = "FSM/Decision/ObjectiveFound")]
public class ObjectiveFound : FSMdecision {

	public override bool Decide(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		return nmn.found;
	}

}
