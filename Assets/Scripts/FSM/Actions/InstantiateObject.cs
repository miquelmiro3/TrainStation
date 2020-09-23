using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InstantiateObject", menuName = "FSM/Action/Instantiate Object")]
public class InstantiateObject : FSMaction {

	public GameObject obj;
	public Vector3 position;

	public override void Act(FSMcontroller controller) {
		controller.InstantiateObject(obj, position);
	}

}
