using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IsAlarmOn", menuName = "FSM/Decision/Is Alarm On")]
public class IsAlarmOn : FSMdecision {

	public override bool Decide(FSMcontroller controller) {
		return StationAlarm.GetAlarmState();
	}

}
