using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IsAutDoorOpen", menuName = "FSM/Decision/Is Automatic Door Open")]
public class IsAutDoorOpen : Decision {

	public override bool Decide(FSMcontroller controller) {
		return controller.customFlag;
	}

}
