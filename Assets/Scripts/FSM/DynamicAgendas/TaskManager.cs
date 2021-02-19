using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public FSMstate actualState = null;
    public FSMstate dynamicState = null;
    public Agenda ag = null;
    public Queue<FSMstate> queue = new Queue<FSMstate>();

    public bool instant = false;

    private FSMcontroller controller;

    public FSMstate GetNextState() {
        if (instant) return dynamicState;
        if (dynamicState != null) {
            dynamicState = null;
            return actualState;
        }
        if (queue.Count > 0) {
            actualState = queue.Dequeue();
            return actualState;
        }
        return null;
    }

    public void AddDynamicState(string lookingForType) {
        dynamicState = Resources.Load("Find" + lookingForType) as FSMstate;

        instant = true;
        PrintFSM();
    }

    public void PrintFSM() {
        string FSM = "";
        if (dynamicState != null) FSM += dynamicState.name + " -> ";
        FSM += actualState.name + " -> ";
        foreach (FSMstate aux in queue)
            FSM += aux.name + " -> ";
        FSM += "FindSomething";

        Debug.Log("Estat de la FSM en aquest punt: " + name);
        Debug.Log(FSM);
    }

    public void createTasks() {
        int numTasks = ag.tasks.Length;

        for (int i = 0; i < numTasks; i++) {
            FSMstate aux = Resources.Load("Find" + ag.tasks[i]) as FSMstate;
            queue.Enqueue(aux);
        }
        actualState = queue.Dequeue();
    }

    public FSMstate GetFirstTask() {
        if (ag != null) {
            if (actualState == null)
                createTasks();
            return actualState;
        }
        return null;
    }

    void Awake() 
    {
        controller = GetComponent<FSMcontroller>();

        TextAsset jsonFile = Resources.Load("Agendas/" + name) as TextAsset;
        if (jsonFile != null)
            ag = JsonUtility.FromJson<Agenda>(jsonFile.text);
    }

    void Update()
    {
        if (Input.GetKeyDown("p") && name == "Female11.1 (1)") {
            PrintFSM();
        }
    }

}
