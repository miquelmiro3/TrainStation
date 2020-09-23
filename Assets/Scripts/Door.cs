using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.XRSimpleInteractable))]
public class Door : Interactuable, IInteractable {

	private bool closedForever = false;

	public void Interact() {
		GetComponent<Animator>().SetTrigger("Open");
		GetComponent<BoxCollider>().enabled=false;
		MakeUninteractuable();
	}

	public void PermanentClose() {
		GetComponent<Animator>().SetTrigger("Close");
		GetComponent<BoxCollider>().enabled=true;
		closedForever=true;
		MakeUninteractuable();
	}

	override protected void Update() {
		if (!closedForever && StationAlarm.instance.alarmOn) PermanentClose();
		base.Update();
	}

	override protected void Start() {
		base.Start();
		Interact();
	}
}
