using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskIsDone", menuName = "FSM/Decision/Task is Done")]
public class TaskIsDone : Decision {

	public override bool Decide(FSMcontroller controller) {
		return TaskHandler.instance.taskDone;
	}

}
