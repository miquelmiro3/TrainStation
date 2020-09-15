using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationAlarm : MonoBehaviour
{

	public bool alarmOn;
	public static StationAlarm instance;

	static public void ActivateAlarm() {
		instance.alarmOn=true;
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
