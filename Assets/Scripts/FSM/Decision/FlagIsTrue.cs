using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Flag is true", menuName = "FSM/Decision/Flag is true")]
public class FlagIsTrue : FSMdecision {

	public int flagID;

	public override bool Decide(FSMcontroller controller) {
		return TaskHandler.instance.flags[flagID];
	}

}
