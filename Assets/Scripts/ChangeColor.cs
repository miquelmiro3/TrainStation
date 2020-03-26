using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

	public Shader standard_shader;
	public Shader change_color_shader;

	private void RecursiveChangeShader(GameObject go, Shader sh) {
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			MeshRenderer mr = child.gameObject.GetComponent<MeshRenderer>();
			if (mr) mr.material.shader=sh;
		}
	}

	public void MakeUnselected() {
		RecursiveChangeShader(gameObject, standard_shader);
	}

	public void MakeSelected() {
		RecursiveChangeShader(gameObject, change_color_shader);
	}

    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		
    }
}
