using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Waiting", menuName = "FSM/Action/Waiting")]
public class Waiting : FSMaction {

	public bool wait;

	public override void Act(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		if (nmn)
			nmn.countDown = wait;
	}
}
