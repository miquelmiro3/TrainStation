using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleInterruption : StateMachineBehaviour {

	private float timer;
	private bool interrupting;
	private bool goingBack;
	private float objectiveX;
	private float objectiveY;
	private float speedX;
	private float speedY;

	private void ResetVariables() {
		timer=Random.Range(15.0f, 30.0f);
		interrupting=false;
		goingBack=false;
		speedX=0.0f;
		speedY=0.0f;
	}

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		ResetVariables();
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (goingBack) {
			if (animator.GetFloat("BlendX")-0.05<=0.0f && animator.GetFloat("BlendY")-0.05f<=0.0f) {
				animator.SetFloat("BlendX", 0.0f);
				animator.SetFloat("BlendY", 0.0f);
				ResetVariables();
				animator.SetTrigger("ChangeIdle");
			} else {
				animator.SetFloat("BlendX", Mathf.SmoothDamp(animator.GetFloat("BlendX"), 0.0f, ref speedX, 1.0f));
				animator.SetFloat("BlendY", Mathf.SmoothDamp(animator.GetFloat("BlendY"), 0.0f, ref speedY, 1.0f));
			}
		} else if (interrupting) {
			if (timer>0.0f) {
				animator.SetFloat("BlendX", Mathf.SmoothDamp(animator.GetFloat("BlendX"), objectiveX, ref speedX, 1.0f));
				animator.SetFloat("BlendY", Mathf.SmoothDamp(animator.GetFloat("BlendY"), objectiveY, ref speedY, 1.0f));
				timer-=Time.deltaTime;
			} else {
				goingBack=true;
				speedX=0.0f;
				speedY=0.0f;
				interrupting=false;
			}
		} else {
			if (timer>0.0f) {
				timer-=Time.deltaTime;
			} else {
				interrupting=true;
				int decision = Random.Range(0, 3);
				switch (decision) {
					case 0:
						objectiveX=0;
						objectiveY=1;
						break;
					case 1:
						objectiveX=1;
						objectiveY=0;
						break;
					case 2:
						objectiveX=1;
						objectiveY=0;
						break;
				}
				timer=6.0f;
			}
		}
	}
}
