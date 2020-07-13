using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactuable {

	public override void Interact() {
		GetComponent<Animator>().SetTrigger("Open");
		GetComponent<BoxCollider>().enabled=false;
		MakeUninteractuable();
	}
}
