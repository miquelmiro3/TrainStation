using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AllowDynamicStates", menuName = "FSM/Action/Allow Dynamic States")]
public class AllowDynamicStates : FSMaction {

	public bool allow;

	public override void Act(FSMcontroller controller) {
		Visibility v = controller.GetComponent<Visibility>();
		if (v)
			v.allowDynamicStates = allow;
	}
}
