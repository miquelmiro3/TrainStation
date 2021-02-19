using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public static Dictionary<string, string[]> tasks;

    //public static Agenda[] agendas;
    public Agenda ag = null;

    public bool dynamicStateAdded = false; // pasar aixo a public

    public FSMstate sameState;
    public FSMstate idleState;

    public FSMstate waitingState;
    public FSMdecision trueDecision;
    public bool instant = false;
    
    private FSMcontroller controller;
    private FSMstate firstState;

    public bool IsDynamicStateAdded() {
        if (dynamicStateAdded) {
            dynamicStateAdded = false;
            return true;
        }
        return false;
    }

    public FSMtransition[] createTransactions(FSMtransition[] originalTrans, FSMstate nextState, bool usingTrueDecision) {
        int numTrans = originalTrans.Length;
        FSMtransition[] trans = (FSMtransition[])originalTrans.Clone();
        
        FSMtransition transNextState = ScriptableObject.CreateInstance("FSMtransition") as FSMtransition;
        if (usingTrueDecision)
            transNextState.decision = trueDecision;
        else
            transNextState.decision = originalTrans[numTrans-1].decision;
        transNextState.destinationState = nextState;
        trans[numTrans-1] = transNextState;

        return trans;
    }

    public void createTasks() {
        int numTasks = ag.tasks.Length;
        FSMstate actualState = null;
        FSMstate originalState = Resources.Load("FindSomething") as FSMstate;
        FSMstate lastState = originalState.Clone();

        for (int i = numTasks-1; i >= 0; i--) {
            string[] aux = tasks[ag.tasks[i]];
            for (int j = aux.Length-1; j >= 0; j--) {
                originalState = Resources.Load(aux[j]) as FSMstate;
                actualState = originalState.Clone();
                actualState.transitions = createTransactions(actualState.transitions, lastState, false);
                lastState = actualState;
            }
        }
        firstState = actualState;

    }

    public FSMstate GetFirstTask() {
        if (ag != null) {
            if (firstState == null)
                createTasks();
            return firstState;
        }
        return null;
    }

    public void PrintFSM() {
        string FSM = "";
        FSMstate actualState = controller.activeState;
        while (actualState.name != "Idle") {
            FSM += actualState.name + " -> ";
            actualState = actualState.transitions[actualState.transitions.Length-1].destinationState;
        }
        FSM += "Idle";
        Debug.Log("Estat de la FSM en aquest punt: " + name);
        Debug.Log(FSM);
    }

    /*public void AddDynamicState(string lookingForType) {
        FSMstate actualState = controller.activeState;
        FSMstate[] vector = new FSMstate[3]{
            actualState.transitions[actualState.transitions.Length-1].destinationState,
            actualState.Clone(),
            actualState.transitions[actualState.transitions.Length-1].destinationState.Clone()
        };
        actualState.name = "Find" + lookingForType;
        FSMstate lastState = vector[vector.Length-1];
        for (int i = vector.Length-2; i >= 0; i--) {
            actualState = vector[i];
            actualState.transitions = createTransactions(actualState.transitions, lastState, false);
            lastState = actualState;
        }

        PrintFSM();
    }*/

    public void AddDynamicState(string lookingForType, string actuallyLookingForType) {
        instant = true;
        FSMstate actualState = controller.activeState;

        List<FSMstate> newTransitions = new List<FSMstate>();
        newTransitions.Add(actualState);
        foreach (string x in tasks[lookingForType]) {
            FSMstate newStates = Resources.Load(x) as FSMstate;
            newTransitions.Add(newStates.Clone());
        }
        if (actuallyLookingForType == "") newTransitions.Add(actualState.Clone());
        else {
            for (int i = 0; i < tasks[actuallyLookingForType].Length; ++i) {
                newTransitions.Add(actualState.Clone());
                actualState = actualState.transitions[actualState.transitions.Length-1].destinationState;
            }
        }

        FSMstate lastState = newTransitions[newTransitions.Count-1];
        for (int i = newTransitions.Count-2; i >= 0; i--) {
            actualState = newTransitions[i];
            actualState.transitions = createTransactions(actualState.transitions, lastState, (i == 0));
            lastState = actualState;
        }

        PrintFSM();
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
