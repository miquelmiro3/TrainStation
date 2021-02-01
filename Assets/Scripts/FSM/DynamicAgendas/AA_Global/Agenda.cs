using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agenda : MonoBehaviour //ScriptableObject
{
    public FSMstate[] taskList;
    public bool working;
    public Queue myTasks = new Queue();

    public void asign() {
        foreach (FSMstate i in taskList)
            myTasks.Enqueue(i);
    }

    public bool noMoreTasks() {
        if (!working && myTasks.Count <= 0) {
            return true;
        }
        return false;
    }

    public bool isNextTask(FSMstate task) {
        if (myTasks.Count <= 0) 
            return false;
        if (!working && (FSMstate)myTasks.Peek() == task) {
            working = true;
            myTasks.Dequeue();
            return true;
        }
        return false;
    }

    public void printMyTasks() {
        foreach (FSMstate idTask in myTasks)
            Debug.Log(idTask);
    }

    public void Start() {
        working = false;
        asign();
    }

}
