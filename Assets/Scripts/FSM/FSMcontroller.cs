using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMcontroller : MonoBehaviour
{
	// The current State in the FSM. The initial value indicates the initial state.
	public State activeState;

	private bool alarm_on;

	private void OnEnable() {
		Alarm.OnAlarmOff+=SetAlarm;
	}

	private void OnDisable() {
		Alarm.OnAlarmOff-=SetAlarm;
	}

	public void SetAlarm(bool state) {
		alarm_on=state;
	}

	public bool IsAlarmOn() {
		return alarm_on;
	}

	private void Start() {
		activeState.OnEnterState(this);
		alarm_on=false;
	}

	private void Update() {
		activeState.PerformConstantActions(this);
		State state = activeState.CheckTransitions(this);
		if (state.stateId!=0) {
			activeState=state;
			activeState.OnEnterState(this);
		}
	}
}
