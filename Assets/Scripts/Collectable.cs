using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactuable, IInteractable
{
	public bool isReady;
	public int flagID;
	// Variable used to indicate a time value to destroy the ticket, to avoid leaving a floating item permanently
	private float timeToDestroy;

	public void Interact() {
		if (flagID>=0) TaskHandler.instance.flags[flagID]=true;
		transform.parent.gameObject.GetComponent<Dispenser>().DelayedMakeInteractuable(0.5f);
		Destroy(gameObject);
	}

	public void TicketReady() {
		isReady=true;
	}

	protected override void Start() {
		base.Start();
		isReady=false;
		timeToDestroy=10.0f;
	}

	protected override void Update() {
		base.Update();
		timeToDestroy-=Time.deltaTime;
		if (timeToDestroy<=0.0f) {
			transform.parent.gameObject.GetComponent<Dispenser>().MakeInteractuable();
			Destroy(gameObject);
		}
	}
}
