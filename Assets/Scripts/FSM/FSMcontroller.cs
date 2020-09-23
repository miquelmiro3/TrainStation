using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMcontroller : MonoBehaviour
{
	// The current State in the FSM. The initial value indicates the initial state.
	public FSMstate activeState;
	// Flag used by some ScriptableObjects in order to communicate certain triggers
	public bool customFlag;

	public void DelayedAction(GameObject holder, FSMaction act, float time) {
		GameObject go = Instantiate(holder);
		go.GetComponent<DelayedAction>().DelayedAct(act, this, time);
	}

	public void InstantiateObject(GameObject obj, Vector3 pos) {
		Instantiate(obj, pos, obj.transform.rotation);
	}

	public void CloseApp() {
		Application.Quit();
	}

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
