using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAction : MonoBehaviour
{

	public FSMaction act;
	public FSMcontroller con;
	public float timer;
	public bool activated=false;

	public void DelayedAct(FSMaction a, FSMcontroller c, float t) {
		act=a;
		con=c;
		timer=t;
		activated=true;
	}

    // Update is called once per frame
    void Update()
    {
		if (activated) {
			timer-=Time.deltaTime;
			if (timer<=0) {
				act.Act(con);
				activated=false;
				Destroy(gameObject);
			}
		}
    }
}
