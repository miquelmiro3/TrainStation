using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChangeAgendaValue", menuName = "FSM/Action/Change Agenda Value")]
public class ChangeAgendaValue : FSMaction {

	public bool value;

	public override void Act(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		if (nmn)
			nmn.agenda=value;
			if (value)
				nmn.Reset();
	}
}
