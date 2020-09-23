using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DoPeoplePanicActivated", menuName = "FSM/Decision/Do People Panic Activated")]
public class DoPeoplePanicActivated : Decision
{

	public override bool Decide(FSMcontroller controller) {
		return StationAlarm.instance.doPeoplePanic;
	}

}
