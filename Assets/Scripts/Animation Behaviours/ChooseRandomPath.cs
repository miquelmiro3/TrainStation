using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandomPath : StateMachineBehaviour {

	public string VariableName;
	public int NumberOfValues;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetInteger(VariableName, Random.Range(0, NumberOfValues));
	}
}
