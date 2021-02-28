using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{

    public string[] taskObjects;
    
    void Awake()
    {
        Visibility.thinkingObjects = new Dictionary<string, GameObject>();
        TaskManager.tasks = new Dictionary<string, FSMstate>();
        foreach (string x in taskObjects) {
            Visibility.thinkingObjects.Add(x, Resources.Load("FindObjects/" + x) as GameObject);
            TaskManager.tasks.Add(x, Resources.Load("Tasks/" + x) as FSMstate);
        }
    }

}
