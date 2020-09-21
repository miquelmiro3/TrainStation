using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : Interactuable, IInteractable
{
	public GameObject collectable;
	public CustomEvent ticketPrinted;
	private float cooldownForSpawn;
	private float timeForSpawn;
	public bool printing;
	public GameObject lastTicket;
	private bool ticketForPlayer;
	public float distanceForPersonInLine;
	private bool machineInUse;
	private float smallCooldownForUse;

	private void SpawnCollectable() {
		lastTicket = Instantiate(collectable, transform, false);
		//if (!ticketForPlayer) lastTicket.layer=LayerMask.NameToLayer("Default");
		//ticketPrinted.ActivateEventTrigger();
	}

	public void AskForTicket() {
		if (!printing) {
			printing=true;
			timeForSpawn=cooldownForSpawn;
			//ticketForPlayer=false;
		}
	}

	public void Interact() {
		if (machineInUse) Debug.Log("It's not your turn");
		else {
			if (!printing) {
				MakeUninteractuable();
				printing=true;
				timeForSpawn=cooldownForSpawn;
				//ticketForPlayer=true;
			}
		}
	}

	override protected void Start() {
		base.Start();
		//ticketPrinted=new CustomEvent();
		timeForSpawn=0.0f;
		cooldownForSpawn=3.0f;
		printing=false;
		//machineInUse=false;
		//smallCooldownForUse=0;
	}

	override protected void Update() {
		base.Update();
		if (printing) {
			timeForSpawn-=Time.deltaTime;
			if (timeForSpawn<=0.0f) {
				SpawnCollectable();
				printing=false;
			}
		}
		/*RaycastHit hit;
		Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0,1,0))*distanceForPersonInLine, Color.cyan);
		if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 1, 0)), out hit, distanceForPersonInLine, LayerMask.GetMask("Person"))) {
			machineInUse=true;
			smallCooldownForUse=1.5f;
		} else {
			if (smallCooldownForUse<=0.0f) machineInUse=false;
			else smallCooldownForUse-=Time.deltaTime;
		}*/
	}
}
