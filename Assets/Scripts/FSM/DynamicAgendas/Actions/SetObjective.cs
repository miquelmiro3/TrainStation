using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetObjective", menuName = "FSM/Action/Set Objective")]
public class SetObjective : FSMaction {

	public bool allowDynamicStates;
	public bool setSpecificObject;
	public string typeOfObject;

	public override void Act(FSMcontroller controller) {
		Visibility v = controller.GetComponent<Visibility>();
		if (setSpecificObject) {
			v.lookingForType = typeOfObject;
			v.setThinkingObject(typeOfObject);
		}
		else {
			v.lookingForType = "";
			v.setThinkingObject("");
		}
		v.allowDynamicStates = allowDynamicStates;
	}
}
