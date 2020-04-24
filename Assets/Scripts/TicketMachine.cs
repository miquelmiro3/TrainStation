using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketMachine : Interactuable
{
	public GameObject ticket;

	public override void Interact() {
		Debug.Log("Giving a ticket");
		MakeUninteractuable();
		Instantiate(ticket, transform, false);
	}
}
