using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DecisionIdle", menuName = "FSM/Decision/KeyPressed")]
public class KeyPressed : Decision
{
	public string key;

	public override bool Decide(FSMcontroller controller) {
		//return Input.GetKeyDown(key);
		return controller.IsAlarmOn();
	}
}
