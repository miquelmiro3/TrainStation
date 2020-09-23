using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ANDdecision", menuName = "FSM/Decision/AND")]
public class ANDdecision : Decision
{
	public Decision d1;
	public Decision d2;

	public override bool Decide(FSMcontroller controller) {
		return d1.Decide(controller) && d2.Decide(controller);
	}
}
