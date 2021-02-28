using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChangeInstantValue", menuName = "FSM/Action/Change Instant Value")]
public class ChangeInstantValue : FSMaction {

	public bool value;

	public override void Act(FSMcontroller controller) {
		TaskManager tm = controller.GetComponent<TaskManager>();
		if (tm) tm.instant=value;
	}
}
