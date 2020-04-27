using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEvent
{
	public event System.Action eventTrigger;

	// Call this function to activate the eventTrigger alarm, triggering all the suscribed functions
	public void ActivateEventTrigger() {
		if (eventTrigger!=null) eventTrigger();
	}
}
