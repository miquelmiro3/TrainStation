using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetAnimationTrigger", menuName = "FSM/Action/Set Animation Trigger")]
public class SetAnimationTrigger : FSMaction {

	public string triggerName;

	public override void Act(FSMcontroller controller) {
		controller.GetComponent<Animator>().SetTrigger(triggerName);
	}

}
