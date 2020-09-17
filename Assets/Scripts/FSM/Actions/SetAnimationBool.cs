using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetAnimationBool", menuName = "FSM/Action/Set Animation Bool")]
public class SetAnimationBool : FSMaction {

	public string boolName;
	public bool value;

	public override void Act(FSMcontroller controller) {
		controller.GetComponent<Animator>().SetBool(boolName,value);
	}

}
