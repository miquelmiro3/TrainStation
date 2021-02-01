using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NextTaskDecision", menuName = "FSM/Decision/NextTask")]
public class NextTaskDecisionGlobal : FSMdecision {

    public FSMstate task;

	public override bool Decide(FSMcontroller controller) {
        Agenda agenda = controller.GetComponent<Agenda>();
		return agenda.isNextTask(task);
	}

}
