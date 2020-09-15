using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SetAlarmOn", menuName = "FSM/Action/Set Alarm ON")]
public class SetAlarmOn : FSMaction {

	public override void Act(FSMcontroller controller) {
		StationAlarm.ActivateAlarm();
	}

}
