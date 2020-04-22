using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionSpeak", menuName = "FSM/Action/Speak")]
public class Speak : Action
{
	public string message;

	override public void Act(FSMcontroller controller) {
		if (condition.Decide(controller)) Debug.Log(message);
	}
}
