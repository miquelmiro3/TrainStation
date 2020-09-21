using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TakeTicketAndLeave", menuName = "FSM/Action/TakeTicketAndLeave")]
public class TakeTicketAndLeave : FSMaction
{
	public float distance;

	public override void Act(FSMcontroller controller) {
		RaycastHit hit;
		if (Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward), out hit, distance, LayerMask.GetMask("Interactuable"))) {
			Dispenser tm = hit.transform.gameObject.GetComponent<Dispenser>();
			if (tm && tm.lastTicket) {
				if (tm.lastTicket.GetComponent<Collectable>().isReady) {
					// TODO: when there's animations in place, make it so it's destroyed at the end of picking up the ticket, and then leave
					Destroy(tm.lastTicket);
					Destroy(controller.gameObject);
				}
			}
		}
	}
}
