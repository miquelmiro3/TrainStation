using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionSpeak", menuName = "FSM/Action/Move")]
public class Move : Action
{
	public float speed;

	public override void Act(FSMcontroller controller) {
		/*if (Input.GetKey(KeyCode.W)) controller.transform.Translate(0, 0, speed);
		if (Input.GetKey(KeyCode.S)) controller.transform.Translate(0, 0, -speed);
		if (Input.GetKey(KeyCode.A)) controller.transform.Translate(-speed, 0, 0);
		if (Input.GetKey(KeyCode.D)) controller.transform.Translate(speed, 0, 0);*/
		controller.transform.Translate(speed,0,0);
	}
}
