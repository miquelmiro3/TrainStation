using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{

	public FSMcontroller fsm;
	private int peopleInside;

    // Start is called before the first frame update
    void Start()
    {
		fsm=GetComponent<FSMcontroller>();
		fsm.customFlag=false;
		peopleInside=0;
    }

	void OnTriggerEnter(Collider other) {
		peopleInside=peopleInside+1;
		fsm.customFlag=true;
	}

	void OnTriggerExit(Collider other) {
		peopleInside=peopleInside-1;
		if (peopleInside==0) fsm.customFlag=false;
	}
}
