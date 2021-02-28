using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public static Dictionary<string, FSMstate> tasks;

    public FSMstate actualState = null;
    public FSMstate dynamicState = null;
    public Agenda agenda = null;
    public Queue<FSMstate> queueStates = new Queue<FSMstate>();

    public bool instant = false;

    public bool IsDynamicState() {
        if (instant) {
            instant = false;
            return true;
        }
        return false;
    }

    public FSMstate GetNextState() {
        if (instant) return dynamicState;
        if (dynamicState != null) {
            dynamicState = null;
            return actualState;
        }
        if (queueStates.Count > 0) {
            actualState = queueStates.Dequeue();
            return actualState;
        }
        return null;
    }

    public void AddDynamicState(string lookingForType) {
        //dynamicState = Resources.Load("Tasks/" + lookingForType) as FSMstate;
        dynamicState = tasks[lookingForType];

        instant = true;
        //PrintFSM();
    }

    public void PrintFSM() {
        string FSM = "";
        if (dynamicState != null) FSM += dynamicState.name + " -> ";
        FSM += actualState.name + " -> ";
        foreach (FSMstate aux in queueStates)
            FSM += aux.name + " -> ";
        FSM += "FindSomething";

        Debug.Log("Estat de la FSM en aquest punt: " + name);
        Debug.Log(FSM);
    }

    public void createTasks() {
        int numTasks = agenda.tasks.Length;

        for (int i = 0; i < numTasks; i++) {
            //FSMstate aux = Resources.Load("Tasks/" + agenda.tasks[i]) as FSMstate;
            //queueStates.Enqueue(aux);
            queueStates.Enqueue(tasks[agenda.tasks[i]]);
        }

        if (queueStates.Count <= 0) {
            FSMstate aux = Resources.Load("Tasks/FindSomething") as FSMstate;
            actualState = aux;
        }
        else {
            actualState = queueStates.Dequeue();
        }
    }

    public FSMstate GetFirstTask() {
        if (agenda != null) {
            if (actualState == null) createTasks();
            return actualState;
        }
        return null;
    }

    void Awake() 
    {
        TextAsset jsonFile = Resources.Load("Agendas/" + name) as TextAsset;
        if (jsonFile != null)
            agenda = JsonUtility.FromJson<Agenda>(jsonFile.text);
    }

    void Update()
    {
        if (Input.GetKeyDown("p") && name == "Female11.1 (1)") {
            PrintFSM();
        }
    }

}
