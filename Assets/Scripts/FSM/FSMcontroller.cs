using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMcontroller : MonoBehaviour
{
	// The current State in the FSM. The initial value indicates the initial state.
	public FSMstate activeState;

	private void Start() {
		activeState.OnEnterState(this);
	}

	private void Update() {
		activeState.PerformConstantActions(this);
		FSMstate state = activeState.CheckTransitions(this);
		if (!state.isStayState) {
			activeState=state;
			activeState.OnEnterState(this);
		}
	}
}
