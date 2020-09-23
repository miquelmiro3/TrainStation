using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DelayedAct", menuName = "FSM/Action/Delayed Act")]
public class DelayedAct : FSMaction {

	public GameObject delayedActionHolder;
	public FSMaction actionToDelay;
	public float timeToDelay;

	public override void Act(FSMcontroller controller) {
		controller.DelayedAction(delayedActionHolder,actionToDelay,timeToDelay);
	}

}
