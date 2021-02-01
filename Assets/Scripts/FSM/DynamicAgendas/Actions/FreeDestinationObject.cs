using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FreeDestinationObject", menuName = "FSM/Action/Free Destination Object")]
public class FreeDestinationObject : FSMaction {

	public bool always;

	public override void Act(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		TaskManager tm = controller.GetComponent<TaskManager>();
		if (nmn && (always || tm.IsDynamicStateAdded()))
			nmn.SetFreeObject();
	}
}
