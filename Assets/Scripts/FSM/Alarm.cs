using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Alarm : MonoBehaviour
{

	public float temps;

	public static event System.Action<bool> OnAlarmOff = delegate { };


    // Update is called once per frame
    void Update()
    {
		temps=temps-Time.deltaTime;
		if (temps<=0) OnAlarmOff.Invoke(true);
    }
}
