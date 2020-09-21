using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
	public static ClickManager instance;
	public float distance_for_interactions; // Indicates the maximum distance to allow an interaction with an object in vr_mode==false
	public bool vr_mode;
	private LayerMask l_mask;
	public GameObject last_selected;

	public void LeftClick() {
		if (instance.last_selected) instance.last_selected.GetComponent<IInteractable>().Interact();
	}

	public void RightClick() {
		Debug.Log("RightClick");
	}

	public void SelectObject(GameObject go) {
		go.GetComponent<Interactuable>().MakeSelected();
		if (instance.last_selected) instance.last_selected.GetComponent<Interactuable>().MakeUnselected();
		instance.last_selected=go;
	}

	public void UnselectObject() {
		if (instance.last_selected) {
			instance.last_selected.GetComponent<Interactuable>().MakeUnselected();
			instance.last_selected=null;
		}
	}

	// Checks if the user is watching an Interactuable object. If they are, call the object's MakeSelected() function. Also calls MakeUnselected() of the previous looked at object
	private void CheckWhatWeWatch() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, distance_for_interactions, l_mask)) {
			GameObject selected = hit.transform.gameObject;
			if (selected!=last_selected) SelectObject(selected);
		} else UnselectObject();
	}

	// Start is called before the first frame update
	void Start()
    {
		ClickManager.instance=this;
        l_mask = LayerMask.GetMask("Interactuable");
		Debug.Log(gameObject.name);
	}

    // Update is called once per frame
    void Update()
    {
		if (!vr_mode) CheckWhatWeWatch();
	}
}
