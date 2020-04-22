using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMaction : ScriptableObject
{
	// The implementation of the Action can optionally use this to decide if it takes place or not.
	public Decision condition;

	abstract public void Act(FSMcontroller controller);
}
