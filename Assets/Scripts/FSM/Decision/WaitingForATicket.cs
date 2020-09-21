using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DecisionWaitingForATicket", menuName = "FSM/Decision/WaitingForATicket")]
public class WaitingForATicket : Decision
{
	public float distance;
	public bool trueWhilePrinting;

	public override bool Decide(FSMcontroller controller) {
		RaycastHit hit;
		if (Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward), out hit, distance, LayerMask.GetMask("Interactuable"))) {
			if (hit.transform.gameObject.GetComponent<Dispenser>()) {
				if (trueWhilePrinting) return hit.transform.gameObject.GetComponent<Dispenser>().printing;
				else return !hit.transform.gameObject.GetComponent<Dispenser>().printing;
			}
		}
		return false;
	}
}
