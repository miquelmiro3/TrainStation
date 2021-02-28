using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DelayedActWithMoreOptions", menuName = "FSM/Action/Delayed Act With More Options")]
public class DelayedActWithMoreOptions : FSMaction {

	public GameObject delayedActionHolder;
	public FSMaction actionToDelay;
	public float timeToDelay;
	public bool randomTime;
	public float maximumTimeToDelay;
	public bool useAnimationLength;
	public AnimationClip animClip;

	public override void Act(FSMcontroller controller) {
		if (randomTime)
			controller.DelayedAction(delayedActionHolder,actionToDelay,Random.Range(timeToDelay,maximumTimeToDelay));
		else if (useAnimationLength)
			controller.DelayedAction(delayedActionHolder,actionToDelay,animClip.length);
		else 
			controller.DelayedAction(delayedActionHolder,actionToDelay,timeToDelay);
	}

}
