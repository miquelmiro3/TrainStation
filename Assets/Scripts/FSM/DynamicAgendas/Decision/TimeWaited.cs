using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TimeWaited", menuName = "FSM/Decision/TimeWaited")]
public class TimeWaited : FSMdecision {

	public override bool Decide(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		return !nmn.countDown;
	}

}
