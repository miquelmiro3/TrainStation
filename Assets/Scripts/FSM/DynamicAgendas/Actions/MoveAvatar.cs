using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MoveAvatar", menuName = "FSM/Action/Move Avatar")]
public class MoveAvatar : FSMaction {

	public float move;
	public float speed;

	public override void Act(FSMcontroller controller) {
		NavMeshNavigator nmn = controller.GetComponent<NavMeshNavigator>();

		if (move >= 0) nmn.moveAvatarDirection = 1;
		else nmn.moveAvatarDirection = -1;
		nmn.moveAvatarDest = controller.transform.position + move * controller.transform.forward;
		nmn.moveAvatarSpeed = speed;
		nmn.moveAvatar = true;
	}
}
