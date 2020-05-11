using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionMove", menuName = "FSM/Action/Move")]
public class Move : FSMaction
{
	public float speed;
	public float distance;

	public override void Act(FSMcontroller controller) {
		RaycastHit hit;
		Debug.DrawRay(controller.transform.position, controller.transform.TransformDirection(Vector3.forward)*distance, Color.yellow);
		if (!Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward), out hit, distance, LayerMask.GetMask("Interactuable", "Person", "Player", "Interactuable2"))) {
			controller.transform.Translate(new Vector3(0, 0, speed*Time.deltaTime));
		} else {
			if (hit.transform.gameObject.layer==LayerMask.NameToLayer("Interactuable")) {
				hit.transform.gameObject.GetComponent<TicketMachine>().AskForTicket();
			}
		}
	}
}
