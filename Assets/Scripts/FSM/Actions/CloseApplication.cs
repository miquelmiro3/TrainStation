using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New CloseApplication", menuName = "FSM/Action/Close Application")]
public class CloseApplication : FSMaction
{

	public override void Act(FSMcontroller controller) {
		controller.CloseApp();
	}
}
