using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEvent : MonoBehaviour
{
	public event System.Action eventTrigger;

	public void OnEventTrigger() {
		if (eventTrigger!=null) eventTrigger();
	}
}
