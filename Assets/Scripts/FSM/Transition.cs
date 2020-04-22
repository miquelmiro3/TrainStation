using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Transition", menuName = "FSM/Transition")]
public class Transition : ScriptableObject
{
	public Decision decision;
	public State destinationState;
}
