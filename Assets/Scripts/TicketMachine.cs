using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketMachine : Interactuable
{
	public GameObject ticket;
	public CustomEvent ticketPrinted;
	private float cooldownForTickets;
	private float timeForTicket;
	public bool printing;
	public GameObject lastTicket;
	private bool ticketForPlayer;

	private void PrintTicket() {
		Debug.Log("Giving a ticket");
		lastTicket = Instantiate(ticket, transform, false);
		if (!ticketForPlayer) lastTicket.layer=LayerMask.NameToLayer("Default");
		ticketPrinted.ActivateEventTrigger();
	}

	public void AskForTicket() {
		if (!printing) {
			printing=true;
			timeForTicket=cooldownForTickets;
			ticketForPlayer=false;
		}
	}

	public override void Interact() {
		if (!printing) {
			MakeUninteractuable();
			printing=true;
			timeForTicket=cooldownForTickets;
			ticketForPlayer=true;
		}
	}

	override protected void Start() {
		base.Start();
		ticketPrinted=new CustomEvent();
		timeForTicket=0.0f;
		cooldownForTickets=3.0f;
		printing=false;
	}

	private void Update() {
		if (printing) {
			timeForTicket-=Time.deltaTime;
			if (timeForTicket<=0.0f) {
				PrintTicket();
				printing=false;
			}
		}
	}
}
