using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StartTrainMove", menuName = "FSM/Action/Start Train Move")]
public class StartTrainMove : FSMaction {

	public string typeOfMove;

	public override void Act(FSMcontroller controller) {
		TrainMoveManager tmm = controller.GetComponent<TrainMoveManager>();
		if (typeOfMove == "enter") 
			tmm.enter = true;
		else if (typeOfMove == "exit")
			tmm.exit = true;
	}
}
