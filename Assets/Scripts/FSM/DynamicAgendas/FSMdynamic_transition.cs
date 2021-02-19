using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dynamic Transition", menuName = "FSM/Transition/DynamicTransition")]
public class FSMdynamic_transition : FSMtransition
{
	public override FSMstate GetDestinationState(FSMcontroller controller) {
		FSMstate aux = controller.GetComponent<TaskManager>().GetNextState();
		if (aux != null) return aux;
		else return destinationState;
	}
}
