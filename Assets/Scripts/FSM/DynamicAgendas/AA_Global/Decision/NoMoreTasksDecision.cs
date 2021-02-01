using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NoMoreTasksDecision", menuName = "FSM/Decision/NoMoreTasks")]
public class NoMoreTasksDecision : FSMdecision {

	public override bool Decide(FSMcontroller controller) {
        Agenda agenda = controller.GetComponent<Agenda>();
		return agenda.noMoreTasks();
	}

}

