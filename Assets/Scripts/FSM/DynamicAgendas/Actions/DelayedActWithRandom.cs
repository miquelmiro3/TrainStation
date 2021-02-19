using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DelayedActWithRandom", menuName = "FSM/Action/Delayed Act With Random")]
public class DelayedActWithRandom : FSMaction {

	public GameObject delayedActionHolder;
	public FSMaction actionToDelay;
	public float timeToDelay;
	public bool randomTime;
	public float maximumTimeToDelay;

	public override void Act(FSMcontroller controller) {
		if (randomTime) controller.DelayedAction(delayedActionHolder,actionToDelay,Random.Range(timeToDelay,maximumTimeToDelay));
		else controller.DelayedAction(delayedActionHolder,actionToDelay,timeToDelay);
	}

}
