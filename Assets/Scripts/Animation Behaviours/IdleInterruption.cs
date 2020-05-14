using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleInterruption : StateMachineBehaviour {

	private float timer;
	private bool done;
	private bool active;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		timer=Random.Range(8.0f, 16.0f);
		done=false;
		active=true;
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		active=false;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (!done && active) {
			if (timer>0.0f) {
				timer-=Time.deltaTime;
			} else {
				done=true;
				animator.SetTrigger("IdleInterruption");
			}
		}
	}
}
