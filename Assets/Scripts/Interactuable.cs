using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{

	private Material original_material;
	public Material highlight_material;
	private MeshRenderer mr;

	private void ChangeMaterial(Material mat) {
		if (mr) mr.material=mat;
	}

	private void GiveScriptToAllChilds() {
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		int i = 0;
		foreach (Transform child in allChildren) {
			if (i > 0) {
				child.gameObject.AddComponent<Interactuable>();
			}
			i++;
		}
	}

	public void MakeUnselectedIndividual() {
		ChangeMaterial(original_material);
	}

	public void MakeUnselected() {
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			child.gameObject.GetComponent<Interactuable>().MakeUnselectedIndividual();
		}
	}

	public void MakeSelectedIndividual() {
		ChangeMaterial(highlight_material);
	}

	public void MakeSelected() {
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			child.gameObject.GetComponent<Interactuable>().MakeSelectedIndividual();
		}
	}

	public void UpdateHighlightMaterialTexture(Material hg) {
		highlight_material=new Material(hg);
		if (mr) highlight_material.SetTexture("OriginalTex", original_material.GetTexture("_BaseMap"));
	}

	public void MakeUninteractuable() {
		gameObject.layer=0;
	}

	public void MakeInteractuable() {
		gameObject.layer=LayerMask.NameToLayer("Interactuable");
	}

	public virtual void Interact() {
		Debug.Log("Error: Interactuable object without Interact() behaviour");
	}

    // Start is called before the first frame update
    void Start()
    {
		GiveScriptToAllChilds();
		mr = GetComponent<MeshRenderer>();
		if (mr) original_material=mr.material;
		if (transform.parent!=null && highlight_material==null) {
			UpdateHighlightMaterialTexture(transform.parent.gameObject.GetComponent<Interactuable>().highlight_material);
		} else {
			UpdateHighlightMaterialTexture(highlight_material);
		}
	}
}
