using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskCompleted", menuName = "FSM/Action/Task Completed")]
public class TaskCompleted : FSMaction
{
	public bool value;

	public override void Act(FSMcontroller controller) {
		TaskHandler.instance.taskDone=value;
	}

}
