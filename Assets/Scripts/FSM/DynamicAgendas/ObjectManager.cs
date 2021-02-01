using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public GameObject[] taskObjects;

    public Dictionary<string, Dictionary<string, Tuple<GameObject, Vector3, bool>>> allObjectPositions;
    /*public Dictionary<string, Tuple<Vector3, bool>> objectPositions = new Dictionary<string, Tuple<Vector3, bool>>(){
        {"Papelera_000", new Tuple<Vector3, bool>(new Vector3(-59f, 180f, 1.5f), false)},
        {"Papelera_001", new Tuple<Vector3, bool>(new Vector3(-52.5f, 180f, 1.5f), false)},
        {"Papelera_008", new Tuple<Vector3, bool>(new Vector3(-40f, 90f, 8f), false)},
        {"Papelera_009", new Tuple<Vector3, bool>(new Vector3(-18.5f, 90f, 11f), false)},
        {"Papelera_010", new Tuple<Vector3, bool>(new Vector3(-18.5f, 90f, -9f), false)},
        {"Papelera_011", new Tuple<Vector3, bool>(new Vector3(-16f, 270f, 11f), false)},
        {"Papelera_012", new Tuple<Vector3, bool>(new Vector3(-16f, 270f, -9f), false)}
    };*/
    
    //se puede llegar a pasar esto a un array i usar el numero de la id como indice, i dentro de un diccionario de types

    //public void Assign() {
    public void Assign(NavMeshNavigator nmn, string type, string id) {
        Dictionary<string, Tuple<GameObject, Vector3, bool>> objects = allObjectPositions[type];
        if (id != "") {
            if (!objects[id].Item3) {
                objects[id] = new Tuple<GameObject, Vector3, bool>(objects[id].Item1, objects[id].Item2, true);
                //nmn.SetDestinationToObject(objects[id].Item2, objects[id].Item1.transform.position, id);
            } 
            else {
                // nmn esta ocupat
                Debug.Log("BOOKED!");
            }
        }
        else {
            List<string> keyList = new List<string>(objects.Keys);
            int numKeys = keyList.Count;
            int initialPos = UnityEngine.Random.Range(0, numKeys);
            int i = initialPos;
            do {
                if (!objects[keyList[i]].Item3)
                    break;
                i = (i+1) % numKeys;
            } while (i != initialPos);

            if (!objects[keyList[i]].Item3) {
                objects[keyList[i]] = new Tuple<GameObject, Vector3, bool>(objects[keyList[i]].Item1, objects[keyList[i]].Item2, true);
                //nmn.SetDestinationToObject(objects[keyList[i]].Item2, objects[keyList[i]].Item1.transform.position, keyList[i]);
            }
            else {
                // nmn esta ocupat
                Debug.Log("TOT BOOKED!");
            }
        }
    }

    public void Free(string type, string id) {
        allObjectPositions[type][id] = new Tuple<GameObject, Vector3, bool>(allObjectPositions[type][id].Item1, allObjectPositions[type][id].Item2, false);
        Debug.Log("Free");
        Debug.Log(id);
    }

    public void BookObject(Visibility vis, string type, List<string> candidates) {
        Dictionary<string, Tuple<GameObject, Vector3, bool>> objects = allObjectPositions[type];
        string choosenName = "";
        foreach (string name in candidates) {
            if (!objects[name].Item3) {
                choosenName = name;
                break;
            }
        }
        if (choosenName != "") vis.ObjectFound(objects[choosenName].Item2, objects[choosenName].Item1.transform.position, choosenName);
    }
    
    void Awake()
    {
        allObjectPositions = new Dictionary<string, Dictionary<string, Tuple<GameObject, Vector3, bool>>>();

        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
        List<GameObject> taskGameObjects = new List<GameObject>();
        foreach (GameObject obj in allGameObjects) {
            if (obj.layer == 15) {
                string typeObj = obj.name.Split('_')[0];

                Transform pos = obj.transform.Find("Pos");
                Tuple<GameObject, Vector3, bool> aux;
                if (pos != null)
                    aux = new Tuple<GameObject, Vector3, bool>(obj, pos.position, false);
                else
                    aux = new Tuple<GameObject, Vector3, bool>(obj, obj.transform.position, false);

                if (allObjectPositions.ContainsKey(typeObj)) 
                    allObjectPositions[typeObj].Add(obj.name, aux);
                else 
                    allObjectPositions.Add(typeObj, new Dictionary<string, Tuple<GameObject, Vector3, bool>>(){ {obj.name, aux} });
                taskGameObjects.Add(obj);
            }
        }
        taskObjects = taskGameObjects.ToArray();

        /*List<string> keyList = new List<string>(allObjectPositions.Keys);// DEBUG
        for (int i = 0; i < keyList.Count; i++) {
            List<string> keyList2 = new List<string>(allObjectPositions[keyList[i]].Keys);
            for (int j = 0; j < keyList2.Count; j++) {
                Debug.Log(keyList2[j]);
            }
        }*/
    }

    void OnEnable() 
    {
        NavMeshNavigator.AssignPosition += Assign;
        NavMeshNavigator.FreePosition += Free;
        Visibility.BookObject += BookObject;
    }

    void OnDisable() 
    {
        NavMeshNavigator.AssignPosition -= Assign;
        NavMeshNavigator.FreePosition -= Free;
        Visibility.BookObject -= BookObject;
    }

}
