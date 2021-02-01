using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChangeWorkingValue", menuName = "FSM/Action/Change Working Value")]
public class ChangeWorkingValue : FSMaction {

	public bool value;

	public override void Act(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		if (nmn)
			nmn.working=value;
	}
}
