using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FreeTaskObject", menuName = "FSM/Action/Free Task Object")]
public class FreeTaskObject : FSMaction {

	public override void Act(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		if (nmn) nmn.SetFreeObject();
	}
}
