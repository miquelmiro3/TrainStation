using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHandler : MonoBehaviour
{

	public static TaskHandler instance;
	public bool[] flags;

    public void Start()
    {
		if (TaskHandler.instance) Destroy(gameObject);
		TaskHandler.instance=this;
    }

}
