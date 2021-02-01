using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DestinationReached", menuName = "FSM/Decision/DestinationReached")]
public class DestinationReached : FSMdecision {

	public override bool Decide(FSMcontroller controller) {
        NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();
		return nmn.destinationReached;
	}

}
