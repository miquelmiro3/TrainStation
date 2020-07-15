using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketMachine : Interactuable, IInteractable
{
	public GameObject ticket;
	public CustomEvent ticketPrinted;
	private float cooldownForTickets;
	private float timeForTicket;
	public bool printing;
	public GameObject lastTicket;
	private bool ticketForPlayer;
	public float distanceForPersonInLine;
	private bool machineInUse;
	private float smallCooldownForUse;

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

	public void Interact() {
		if (machineInUse) Debug.Log("It's not your turn");
		else {
			if (!printing) {
				MakeUninteractuable();
				printing=true;
				timeForTicket=cooldownForTickets;
				ticketForPlayer=true;
			}
		}
	}

	override protected void Start() {
		base.Start();
		ticketPrinted=new CustomEvent();
		timeForTicket=0.0f;
		cooldownForTickets=3.0f;
		printing=false;
		machineInUse=false;
		smallCooldownForUse=0;
	}

	override protected void Update() {
		base.Update();
		if (printing) {
			timeForTicket-=Time.deltaTime;
			if (timeForTicket<=0.0f) {
				PrintTicket();
				printing=false;
			}
		}
		RaycastHit hit;
		Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0,1,0))*distanceForPersonInLine, Color.cyan);
		if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 1, 0)), out hit, distanceForPersonInLine, LayerMask.GetMask("Person"))) {
			machineInUse=true;
			smallCooldownForUse=1.5f;
		} else {
			if (smallCooldownForUse<=0.0f) machineInUse=false;
			else smallCooldownForUse-=Time.deltaTime;
		}
	}
}
