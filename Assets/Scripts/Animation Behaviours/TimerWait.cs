using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerWait : StateMachineBehaviour
{
	private float timer;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		timer=Random.Range(0.0f, 7.0f);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (timer>0.0f) {
			timer-=Time.deltaTime;
		} else {
			animator.SetBool("ActivateAnimations", true);
		}
	}
}
