using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChangeAnimationSpeed", menuName = "FSM/Action/Change Animation Speed")]
public class ChangeAnimationSpeed : FSMaction
{

	public float animSpeedMin;
	public float animSpeedMax;

	public override void Act(FSMcontroller controller) {
		controller.GetComponent<Animator>().speed=Random.Range(animSpeedMin, animSpeedMax);
	}

}
