using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IsDynamicState", menuName = "FSM/Decision/Is Dynamic State")]
public class IsDynamicState : FSMdecision {

	public override bool Decide(FSMcontroller controller) {
		TaskManager tm = controller.GetComponent<TaskManager>();
		return tm.IsDynamicState();
	}

}
