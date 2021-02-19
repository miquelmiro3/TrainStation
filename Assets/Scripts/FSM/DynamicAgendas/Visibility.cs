using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    public static Dictionary<string, GameObject> thinkingObjects;
    private bool showThinkingObject = true;
    private GameObject thinkingObject = null;


    public float viewDistance;
    public float viewAngle;
    public bool allowDynamicStates = false;
    public string lookingForType = "";
    public string lastType = "";

    private List<GameObject> visibleObjects;
    private float delay;

    
    public GameObject sphere;//DEBUG
    private List<GameObject> spheres = new List<GameObject>();//DEBUG

    public delegate void VisibilityBookObject(Visibility vis, string type, List<string> candidates); //TEMA EVENTS
	public static event VisibilityBookObject BookObject;

    public void setThinkingObject(string s) {
        if (thinkingObject) Destroy(thinkingObject);

        if (s != "" && (!thinkingObject || thinkingObject.tag != s)) {
            Vector3 aux = transform.position;
            aux.y = 2.5f;
            thinkingObject = Instantiate(thinkingObjects[s], aux, transform.rotation);
            thinkingObject.SetActive(showThinkingObject);
            thinkingObject.transform.parent = transform;
        }
    }

    public void ObjectFound(bool setDestination, GameObject obj, Vector3 dest, int queuePos, float rot) { //CANVIAR OBJ PER EL GAMEOBJECT
        lastType = obj.tag;
        lookingForType = "";
        allowDynamicStates = false;
        setThinkingObject(obj.tag);
        NavMeshNavigator nmn = GetComponent<NavMeshNavigator>();
        nmn.ObjectFound(obj, dest, queuePos, rot);
        if (setDestination) nmn.SetDestinationToObject();
    }

    private void RandomObjective() {
        if (visibleObjects.Count > 0 && UnityEngine.Random.Range(0.0f, 1.0f) < 0.05f) {
            // escollir un objecte visible i cridar al taskmanager perque creei un estat
            List<string> aux = new List<string>();
            foreach (GameObject obj in visibleObjects) aux.Add(obj.name);
            //BookObject.Invoke(this, "", aux);
            //GetComponent<TaskManager>() perque repeteixi la tasca despres

            foreach (GameObject obj in visibleObjects) {
                if (obj.tag != lastType) {
                    IndividualObjectManager iom = obj.GetComponent<IndividualObjectManager>();
                    if (iom) {
                        Tuple<Vector3, int, float> bookedObj = iom.Book(GetComponent<NavMeshNavigator>());
                        if (bookedObj != null) {
                            ObjectFound(false, obj, bookedObj.Item1, bookedObj.Item2, bookedObj.Item3);
                            GetComponent<TaskManager>().AddDynamicState(obj.tag, lookingForType);
                            break;
                        }
                    }
                    else {
                        Vector3 dir = (transform.position - obj.transform.position).normalized;
                        Vector3 newDest = obj.transform.position + dir * 0.5f;
                        ObjectFound(false, obj, newDest, 0, -1);
                        GetComponent<TaskManager>().AddDynamicState(obj.tag, lookingForType);
                        break;
                    }
                }
            }

            Debug.Log("DynamicState!!");
        }
    }

    public void LookVisibleObjects() {
        
    }

    IEnumerator FindVisibleObjects() {
        while (true) {
            if (sphere != null) {
                for (int i = 0; i < spheres.Count; i++) //DEBUG
                    Destroy(spheres[i]);
                spheres = new List<GameObject>();
            }
            
            if (allowDynamicStates) {
                VisibleObjects();
                if (lookingForType != "") {
                    List<GameObject> aux = new List<GameObject>();
                    foreach (GameObject obj in visibleObjects) { //POTSER CANVIAR LA FORMA DE ESCOLLIR A RANDOM
                        if (obj.tag == lookingForType) aux.Add(obj);
                    }
                    if (aux.Count > 0) {
                        foreach (GameObject obj in aux) { //SI ENS QUEDEM AMB AIXO PASAR-HO A DALT
                            IndividualObjectManager iom = obj.GetComponent<IndividualObjectManager>();
                            if (iom) {
                                Tuple<Vector3, int, float> bookedObj = iom.Book(GetComponent<NavMeshNavigator>());
                                if (bookedObj != null) {
                                    ObjectFound(true, obj, bookedObj.Item1, bookedObj.Item2, bookedObj.Item3);
                                    break;
                                }
                            }
                            else {
                                Vector3 dir = (transform.position - obj.transform.position).normalized;
                                Vector3 newDest = obj.transform.position + dir * 0.5f;
                                ObjectFound(true, obj, newDest, 0, -1);
                                break;
                            }

                        }
                        //BookObject.Invoke(this, lookingForType, aux);
                    }
                    else RandomObjective();
                }
                else RandomObjective();
            }

            yield return new WaitForSeconds(delay); //un poco de randomness
        }
    }

    void VisibleObjects() {
        visibleObjects = new List<GameObject>();

        Vector3 position = transform.position;
        Collider[] posibleVisibleObjects = Physics.OverlapSphere(position, viewDistance, 1<<15);

        //for (int i = 0; i < posibleVisibleObjects.Length; i++) Debug.Log("In range " + posibleVisibleObjects[i].name);

        Vector3 auxPosition = position;
        auxPosition.y = 1;
        for (int i = 0; i < posibleVisibleObjects.Length; i++) {
            Vector3 objectPosition = posibleVisibleObjects[i].transform.position;
            Vector3 auxObjectPosition = objectPosition;
            auxObjectPosition.y = 1;
            Vector3 directionToObject = (auxObjectPosition - auxPosition).normalized;
            if (Vector3.Angle(transform.forward, directionToObject) < viewAngle / 2) {
                RaycastHit hit;
                directionToObject = (objectPosition - auxPosition).normalized;
                if (Physics.Raycast(auxPosition, directionToObject, out hit, viewDistance, 0b1000_1111_0000_0001)) {
                    //Debug.Log("Hitting " + hit.collider.name);
                    if (hit.collider == posibleVisibleObjects[i]) {
                        //Debug.Log("Seeing " + posibleVisibleObjects[i].name);
                        visibleObjects.Add(hit.collider.gameObject);

                        if (sphere != null) {
                            Vector3 aux = objectPosition;//DEBUG
                            aux.y = 2.5f;
                            spheres.Add(Instantiate(sphere, aux, new Quaternion()));
                        }
                    }
                }
            }
        }
    }
 
    void Start() 
    {
        delay = 0.5f;// + UnityEngine.Random.Range(0.0f, 0.5f);
        StartCoroutine("FindVisibleObjects");
    }

    void Update()
    {
        if (Input.GetKeyDown("t")) {
            showThinkingObject = !showThinkingObject;
            if (thinkingObject != null) thinkingObject.SetActive(showThinkingObject);
        }
    }

}
