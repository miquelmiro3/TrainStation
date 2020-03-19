using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

	public float distance_for_interactions; // Indicates the maximum distance to allow an interaction with an object
	private LayerMask l_mask;
	private GameObject last_selected;

	public void Click() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // TODO: comprobar que funciona con VR, donde quizas no existe un "mousePosition"
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, distance_for_interactions, l_mask)) {
			Debug.Log("You touch the cube");
		}
	}

	public void LeftClick() {
		Debug.Log("LeftClick");
		Click();
	}

	public void RightClick() {
		Debug.Log("RightClick");
		Click();
	}

	private void CheckWhatWeWatch() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // TODO: comprobar que funciona con VR, donde quizas no existe un "mousePosition"
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, distance_for_interactions, l_mask)) {
			GameObject selected = hit.transform.gameObject;
			selected.GetComponent<ChangeColor>().MakeSelected();
			if (selected!=last_selected) {
				if (last_selected) last_selected.GetComponent<ChangeColor>().MakeUnselected();
				last_selected=selected;
			}
		} else if (last_selected) {
			last_selected.GetComponent<ChangeColor>().MakeUnselected();
			last_selected=null;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        l_mask = LayerMask.GetMask("Interactuable");
	}

    // Update is called once per frame
    void Update()
    {
		CheckWhatWeWatch();
	}
}
