using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.XRSimpleInteractable))]
public class Door : Interactuable, IInteractable {

	public void Interact() {
		GetComponent<Animator>().SetTrigger("Open");
		GetComponent<BoxCollider>().enabled=false;
		MakeUninteractuable();
	}

	override protected void Start() {
		base.Start();
		Interact();
	}
}
