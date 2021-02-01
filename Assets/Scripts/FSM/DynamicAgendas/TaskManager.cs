using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public bool dynamicStateAdded = false; // pasar aixo a public

    public FSMstate sameState;
    public FSMstate idleState;

    public FSMstate waitingState;
    public FSMdecision trueDecision;
    public bool instant = false;

    public bool dynamicStateWait = true;
    private FSMstate taskBin;

    public FSMstate[] myTasks;
    
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
        int numTasks = myTasks.Length;
        FSMstate actualState = null;
        FSMstate lastState = idleState;

        for (int i = numTasks-1; i >= 0; i--) {
            //FSMstate originalState = Resources.Load(myTasks[i]) as FSMstate; //myTasks[i] string nom estat
            //actualState = originalState.Clone();
            actualState = myTasks[i].Clone();
            actualState.transitions = createTransactions(actualState.transitions, lastState, false);
            lastState = actualState;
        }
        firstState = actualState;
    }

    public FSMstate GetFirstTask() {
        if (myTasks.Length > 0){
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
        Debug.Log("Estat de la FSM en aquest punt:");
        Debug.Log(FSM);
    }

    void Start() 
    {
        controller = GetComponent<FSMcontroller>();
        taskBin = Resources.Load("TaskBin") as FSMstate; 
    }

    void Update()
    {
        if (Input.GetKeyDown("space")) {
            Debug.Log("Space");
            dynamicStateAdded = true;

            FSMstate actualState = controller.activeState;
            FSMstate nextState = actualState.transitions[actualState.transitions.Length-1].destinationState;
            FSMstate newState;
            if (dynamicStateWait) newState = waitingState.Clone();
            else newState = taskBin.Clone();

            if (instant) {
                newState.name += " (dynamic+instant)";
                FSMstate repeatState = actualState.Clone();
                repeatState.transitions = createTransactions(actualState.transitions, nextState, false);
                newState.transitions = createTransactions(newState.transitions, repeatState, false);
                actualState.transitions = createTransactions(actualState.transitions, newState, instant);
            }
            else {
                newState.name += " (dynamic)";
                newState.transitions = createTransactions(newState.transitions, nextState, false);
                actualState.transitions = createTransactions(actualState.transitions, newState, instant);
            }

            PrintFSM();
        }
    }

}
