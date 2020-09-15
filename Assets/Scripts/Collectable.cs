using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactuable, IInteractable
{
	public bool isReady;
	public int flagID;
	// Variable used to indicate a time value to destroy the ticket, to avoid people getting stuck in lines because of the player's actions
	private float timeToDestroy;

	public void Interact() {
		Debug.Log("You get a ticket");
		if (flagID>=0) TaskHandler.instance.flags[flagID]=true;
		transform.parent.gameObject.GetComponent<TicketMachine>().DelayedMakeInteractuable(0.5f);
		Destroy(gameObject);
	}

	public void TicketReady() {
		isReady=true;
	}

	protected override void Start() {
		base.Start();
		isReady=false;
		timeToDestroy=5.0f;
	}

	protected override void Update() {
		base.Update();
		timeToDestroy-=Time.deltaTime;
		if (timeToDestroy<=0.0f) {
			Debug.Log("Remember to pick up the ticket... We've disposed of one you left in the machine.");
			transform.parent.gameObject.GetComponent<TicketMachine>().MakeInteractuable();
			Destroy(gameObject);
		}
	}
}
