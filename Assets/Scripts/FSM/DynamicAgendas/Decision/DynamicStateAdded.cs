using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dynamic State Added", menuName = "FSM/Decision/DynamicStateAdded")]
public class DynamicStateAdded : FSMdecision {

	public override bool Decide(FSMcontroller controller) {
		TaskManager tm = controller.GetComponent<TaskManager>();
		return tm.instant;
	}

}
