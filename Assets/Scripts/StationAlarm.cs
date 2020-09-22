using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationAlarm : MonoBehaviour
{

	public bool alarmOn;
	public GameObject lightAnimator;
	public static StationAlarm instance;

	static public void ActivateAlarm() {
		instance.alarmOn=true;
		instance.lightAnimator.GetComponent<Animator>().enabled=true;
	}

	static public bool GetAlarmState() {
		return instance.alarmOn;
	}

    // Start is called before the first frame update
    void Start()
    {
		alarmOn=false;
		if (StationAlarm.instance) Destroy(gameObject);
		StationAlarm.instance=this;
    }

}
