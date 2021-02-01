using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMcontroller : MonoBehaviour
{
	// The current State in the FSM. The initial value indicates the initial state.
	public FSMstate activeState;
	// Flag used by some ScriptableObjects in order to communicate certain triggers
	[HideInInspector] public bool customFlag;

	public void DelayedAction(GameObject holder, FSMaction act, float time) {
		GameObject go = Instantiate(holder);
		go.GetComponent<DelayedAction>().DelayedAct(act, this, time);
	}

	public void InstantiateObject(GameObject obj, Vector3 pos) {
		Instantiate(obj, pos, obj.transform.rotation);
	}

	public void CloseApp() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	private void Start() {
		TaskManager tm = GetComponent<TaskManager>(); //NEW
		if (tm) {
			FSMstate firstTask = tm.GetFirstTask();
			if (firstTask)
				activeState = firstTask;
		}
		if(name == "Female11.1 (1)") print(activeState); //DEBUG

		activeState.OnEnterState(this);
	}

	private void Update() {
		activeState.PerformConstantActions(this);
		FSMstate state = activeState.CheckTransitions(this);
		if (!state.isStayState) {
			activeState.OnLeaveState(this);//DEBUG
			activeState=state;
			activeState.OnEnterState(this);

			if(name == "Female11.1 (1)") print(activeState); //DEBUG
		}
	}
}
