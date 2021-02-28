using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FinishedTrainMove", menuName = "FSM/Decision/Finished Train Move")]
public class FinishedTrainMove : FSMdecision {

	public string typeOfMove;

	public override bool Decide(FSMcontroller controller) {
        TrainMoveManager tmm = controller.GetComponent<TrainMoveManager>();
		if (typeOfMove == "enter") 
			return !tmm.enter;
		else if (typeOfMove == "exit")
			return !tmm.exit;
		else 
			return false;
	}

}
