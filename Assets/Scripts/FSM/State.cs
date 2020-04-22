using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "FSM/State")]
public class State : ScriptableObject
{
	// The id of the state. It's important that the id 0 is used for the stayState.
	public int stateId;
	// The state with id 0, to indicate the controller that there's no transitions to be made.
	public State stayState;
	// Array of Actions that take place when transitioning into this state.
	public Action[] onEnterActions;
	// Array of Actions that take place constantly, each frame, while in this state.
	public Action[] constantActions;
	// The order in which the transitions are stored indicates the priority: for a given frame,
	// the first transition which returns a true value for their decision will be the transition that takes place
	public Transition[] transitions;

	// Function called by the controller to perform all onEnterActions
	public void OnEnterState(FSMcontroller controller) {
		foreach (Action action in onEnterActions) {
			action.Act(controller);
		}
	}

	// Function called by the controller to perform all constantActions
	public void PerformConstantActions(FSMcontroller controller) {
		foreach (Action action in constantActions) {
			action.Act(controller);
		}
	}

	// Function called by the controller to return which state we are gonna move into
	public State CheckTransitions(FSMcontroller controller) {
		foreach (Transition transition in transitions) {
			if (transition.decision.Decide(controller)) {
				return transition.destinationState;
			}
		}
		return stayState;
	}
}
