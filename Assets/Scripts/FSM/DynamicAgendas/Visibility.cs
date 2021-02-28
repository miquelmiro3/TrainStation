using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    public static Dictionary<string, GameObject> thinkingObjects;
    private bool showThinkingObject = true;
    private GameObject thinkingObject = null;
    private string[] tasksWithoutObject = new string[1]{"TalkingPhone"};

    public static float viewDistance = 15f;
    public static float viewAngle = 90f;
    public bool allowDynamicStates = false;
    public string lookingForType = "";
    public string lastType = "";

    private List<GameObject> visibleObjects;
    private float delay;

    public GameObject sphere;//DEBUG
    private List<GameObject> spheres = new List<GameObject>();//DEBUG

    public void setThinkingObject(string s) {
        if (thinkingObject) Destroy(thinkingObject);

        if (s != "") {
            Vector3 thinkingObjectPosition = transform.position;
            thinkingObjectPosition.y = 2.5f;
            thinkingObject = Instantiate(thinkingObjects[s], thinkingObjectPosition, transform.rotation);
            thinkingObject.SetActive(showThinkingObject);
            thinkingObject.transform.parent = transform;
        }
    }

    public void ObjectFound(GameObject obj, Vector3 dest, int queuePos, float rot) {
        lastType = obj.tag;
        lookingForType = "";
        allowDynamicStates = false;
        setThinkingObject(obj.tag);
        GetComponent<NavMeshNavigator>().ObjectFound(obj, dest, queuePos, rot);
    }

    private void RandomObjective() {
        if (visibleObjects.Count > 0 && UnityEngine.Random.Range(0.0f, 1.0f) < 0.05f) {
            foreach (GameObject obj in visibleObjects) {
                if (obj.tag != lastType) {
                    IndividualObjectManager iom = obj.GetComponent<IndividualObjectManager>();
                    if (iom) {
                        Tuple<Vector3, int, float> bookedObj = iom.Book(GetComponent<NavMeshNavigator>());
                        if (bookedObj != null) {
                            ObjectFound(obj, bookedObj.Item1, bookedObj.Item2, bookedObj.Item3);
                            GetComponent<TaskManager>().AddDynamicState(obj.tag);
                            break;
                        }
                    }
                    else {
                        Vector3 dir = (transform.position - obj.transform.position).normalized;
                        Vector3 newDest = obj.transform.position + dir * 0.5f;
                        ObjectFound(obj, newDest, 0, -1);
                        GetComponent<TaskManager>().AddDynamicState(obj.tag);
                        break;
                    }
                }
            }
            //Debug.Log("DynamicState!!");
        }
        else if (visibleObjects.Count == 0 && UnityEngine.Random.Range(0.0f, 1.0f) < 0.0025f) { //TASKS THAT ARENT VINCULATED TO AN OBJECT
            // POTSER ES POT FER AMB OBJFOUND (SERIA MES SENZILL)
            int i = UnityEngine.Random.Range(0, tasksWithoutObject.Length-1);
            lastType = tasksWithoutObject[i];
            lookingForType = "";
            allowDynamicStates = false;
            setThinkingObject(tasksWithoutObject[i]);
            GetComponent<TaskManager>().AddDynamicState(tasksWithoutObject[i]);
        }
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
                    List<GameObject> posibleObjects = new List<GameObject>();
                    foreach (GameObject obj in visibleObjects) { //POTSER CANVIAR LA FORMA DE ESCOLLIR A RANDOM
                        if (obj.tag == lookingForType)
                            posibleObjects.Add(obj);
                    }
                    foreach (GameObject obj in posibleObjects) { //SI ENS QUEDEM AMB AIXO PASAR-HO A DALT
                        IndividualObjectManager iom = obj.GetComponent<IndividualObjectManager>();
                        if (iom) {
                            Tuple<Vector3, int, float> bookedObj = iom.Book(GetComponent<NavMeshNavigator>());
                            if (bookedObj != null) {
                                ObjectFound(obj, bookedObj.Item1, bookedObj.Item2, bookedObj.Item3);
                                break;
                            }
                        }
                        else {
                            Vector3 dir = (transform.position - obj.transform.position).normalized;
                            Vector3 newDest = obj.transform.position + dir * 0.5f;
                            ObjectFound(obj, newDest, 0, -1);
                            break;
                        }
                    }
                    if (posibleObjects.Count <= 0) RandomObjective();
                }
                else RandomObjective();
            }

            yield return new WaitForSeconds(delay);
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
