using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StartTrainCountdown", menuName = "FSM/Action/Start Train Countdown")]
public class StartTrainCountdown : FSMaction {

	public float minTime;
	public float maxTime;

	public override void Act(FSMcontroller controller) {
		TrainMoveManager tmm = controller.GetComponent<TrainMoveManager>();
		tmm.StartCountdown(Random.Range(minTime, maxTime));
	}
}
