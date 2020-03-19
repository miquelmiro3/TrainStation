using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

	private Material unselected_material;
	public Material selected_material;

	public void MakeUnselected() {
		GetComponent<MeshRenderer>().material=unselected_material;
	}

	public void MakeSelected() {
		GetComponent<MeshRenderer>().material=selected_material;
	}

    // Start is called before the first frame update
    void Start()
    {
		unselected_material = GetComponent<MeshRenderer>().material;
	}

    // Update is called once per frame
    void Update()
    {
		
    }
}
