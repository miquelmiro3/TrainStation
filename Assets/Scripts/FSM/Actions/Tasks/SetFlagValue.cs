using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SetFlagValue", menuName = "FSM/Action/Set Flag Value")]
public class SetFlagValue : FSMaction
{
	public int flagID;
	public bool value;

	public override void Act(FSMcontroller controller) {
		TaskHandler.instance.flags[flagID]=value;
	}

}
