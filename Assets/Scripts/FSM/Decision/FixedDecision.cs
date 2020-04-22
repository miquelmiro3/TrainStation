using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FixedDecision", menuName = "FSM/Decision/FixedDecision")]
public class FixedDecision : Decision
{
	public bool fixedValue;

	public override bool Decide(FSMcontroller controller) {
		return fixedValue;
	}
}
