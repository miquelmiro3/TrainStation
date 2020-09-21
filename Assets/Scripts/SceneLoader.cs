using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Interactuable, IInteractable {

	public int scene;

	public void Interact() {
		SceneManager.LoadScene(scene);
	}

}
