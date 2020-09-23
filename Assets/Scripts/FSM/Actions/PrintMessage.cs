using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PrintMessage", menuName = "FSM/Action/Print Message")]
public class PrintMessage : FSMaction {

	public string message;
	public float duration;

	public override void Act(FSMcontroller controller) {
		MessageVR.PrintMessage(message, duration);
	}

}
