using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Transition", menuName = "FSM/Transition")]
public class FSMtransition : ScriptableObject
{
	public FSMdecision decision;
	public FSMstate destinationState;
}
