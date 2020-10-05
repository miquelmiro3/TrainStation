using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMdecision : ScriptableObject
{
	public abstract bool Decide(FSMcontroller controller);
}
