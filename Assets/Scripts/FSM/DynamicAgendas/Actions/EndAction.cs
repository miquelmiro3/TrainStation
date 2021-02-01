using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EndAction", menuName = "FSM/Action/EndAction")]
public class EndAction : FSMaction {

	public override void Act(FSMcontroller controller) {
		Debug.Log("End of state");
	}
}
