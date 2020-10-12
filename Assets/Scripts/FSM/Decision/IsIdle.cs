using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IsIdle", menuName = "FSM/Decision/Is Idle")]
public class IsIdle : FSMdecision {

	public bool negate;

	public override bool Decide(FSMcontroller controller) {
		if (negate) return !controller.GetComponent<NavMeshNavigator>().idle;
		return controller.GetComponent<NavMeshNavigator>().idle;
	}

}
