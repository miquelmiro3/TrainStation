using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IsAlarmOn", menuName = "FSM/Decision/Is Alarm On")]
public class IsAlarmOn : Decision
{

	public StationAlarm alarm;

	public override bool Decide(FSMcontroller controller) {
		return alarm.GetAlarmState();
	}

}
