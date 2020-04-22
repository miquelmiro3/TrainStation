using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : Interactuable
{
	public override void Interact() {
		Debug.Log("You get a ticket");
		Destroy(gameObject);
	}
}
