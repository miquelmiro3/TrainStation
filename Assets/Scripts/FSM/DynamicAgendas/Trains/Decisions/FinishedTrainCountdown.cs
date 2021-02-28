using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FinishedTrainCountdown", menuName = "FSM/Decision/Finished Train Countdown")]
public class FinishedTrainCountdown : FSMdecision {

	public override bool Decide(FSMcontroller controller) {
        TrainMoveManager tmm = controller.GetComponent<TrainMoveManager>();
		return !tmm.countdown;
	}

}
