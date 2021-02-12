using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetAnimationTrigger", menuName = "FSM/Action/Set Animation Trigger")]
public class SetAnimationTrigger : FSMaction {

	public string triggerName;
	public string[] resetTriggers;

	public override void Act(FSMcontroller controller) {
		Animator anim = controller.GetComponent<Animator>();

		foreach (string x in resetTriggers)
			anim.ResetTrigger(x);
		
		anim.SetTrigger(triggerName);
	}

}
