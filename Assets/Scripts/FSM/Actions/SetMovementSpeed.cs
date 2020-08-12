using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetMovementSpeed", menuName = "FSM/Action/Set Movement Speed")]
public class SetMovementSpeed : FSMaction
{

	public float speedMin;
	public float speedMax;

	public override void Act(FSMcontroller controller) {
		controller.GetComponent<NavMeshNavigator>().SetSpeed(Random.Range(speedMin,speedMax));
	}

}
