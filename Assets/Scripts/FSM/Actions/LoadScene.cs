using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New LoadScene", menuName = "FSM/Action/Load Scene")]
public class LoadScene : FSMaction
{
	public int sceneId;

	public override void Act(FSMcontroller controller) {
		SceneManager.LoadScene(sceneId);
	}
}
