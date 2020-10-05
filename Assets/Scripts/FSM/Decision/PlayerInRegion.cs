using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerInRegion", menuName = "FSM/Decision/Player in region")]
public class PlayerInRegion : FSMdecision {

	public Vector3 pointA;
	public Vector3 pointB;

	public override bool Decide(FSMcontroller controller) {
		Vector3 pl = PlayerPosition.instance.position;
		if ((pl.x>=pointA.x && pl.x<=pointB.x) || (pl.x>=pointB.x && pl.x<=pointA.x)) {
			if ((pl.z>=pointA.z && pl.z<=pointB.z) || (pl.z>=pointB.z && pl.z<=pointA.z)) {
				return true;
			}
		}
		return false;
	}

}
