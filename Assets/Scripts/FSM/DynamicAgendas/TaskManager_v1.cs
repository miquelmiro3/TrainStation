using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager_v1 : MonoBehaviour
{
    public FSMstate sameState;
    public FSMstate idleState;

    public FSMstate waitingState;
    public FSMdecision trueDecision;
    public bool usePosition = false;
    public bool instant = false;
    public int position;

    public FSMstate[] myTasks;
    //public FSMstate[] listTask;
    public List<FSMstate> listTask;

    //private adjacencyList;
    
    private bool ready = false; 

    public FSMtransition[] createTransactions(FSMtransition[] originalTrans, FSMstate nextState, bool usingTrueDecision) {
        int numTrans = originalTrans.Length;
        FSMtransition[] trans = new FSMtransition[numTrans];
        for (int j = 0; j < numTrans-1; j++)
            trans[j] = originalTrans[j];
        
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
        //listTask = new FSMstate[numTasks];
        listTask = new List<FSMstate>(new FSMstate[numTasks]);
        for (int i = numTasks-1; i >= 0; i--) {
            listTask[i] = myTasks[i].Clone();
            if (i != numTasks-1)
                listTask[i].transitions = createTransactions(myTasks[i].transitions, listTask[i+1], false);
            else
                listTask[i].transitions = createTransactions(myTasks[i].transitions, idleState, false);
        }
        ready = true;
    }

    public FSMstate GetFirstTask() {
        if (myTasks.Length > 0){
            if (!ready)
                createTasks();
            return listTask[0];
        }
        return null;
    }

    public void Update()
    {
        if (Input.GetKeyDown("space")) {
            Debug.Log("Space");
            //listTask.Add(ScriptableObject.CreateInstance("FSMstate") as FSMstate);

            int i;
            if (usePosition) i = position;
            else i = listTask.Count;

            FSMstate dyanmicState = waitingState.Clone();
            if (instant)
                dyanmicState.name += " (dynamic+instant)";
            else
                dyanmicState.name += " (dynamic)";

            listTask.Insert(i, dyanmicState);
            if (i != listTask.Count-1)
                listTask[i].transitions = createTransactions(waitingState.transitions, listTask[i+1], false);
            else
                listTask[i].transitions = createTransactions(waitingState.transitions, idleState, false);
            
            i -= 1;
            if (i >= 0) {
                if (i != listTask.Count-1)
                    listTask[i].transitions = createTransactions(listTask[i].transitions, listTask[i+1], instant);
                else
                    listTask[i].transitions = createTransactions(listTask[i].transitions, idleState, instant);
            }
        }
    }

}
