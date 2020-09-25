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

	private void SpawnCollectable() {
		lastTicket = Instantiate(collectable, transform, false);
	}

	public void AskForTicket() {
		if (!printing) {
			printing=true;
			timeForSpawn=cooldownForSpawn;
		}
	}

	public void Interact() {
		if (!printing) {
			MakeUninteractuable();
			printing=true;
			timeForSpawn=cooldownForSpawn;
		}
	}

	override protected void Start() {
		base.Start();
		timeForSpawn=0.0f;
		cooldownForSpawn=3.0f;
		printing=false;
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
	}
}
