using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{

    public bool allowDynamicStates = false;
    
    public GameObject sphere;//DEBUG
    private List<GameObject> spheres = new List<GameObject>();//DEBUG
    private List<GameObject> visibleObjects;

    public float viewDistance;
    public float viewAngle;

    public string lookingForType;

    public delegate void VisibilityBookObject(Visibility vis, string type, List<string> candidates); //TEMA EVENTS
	public static event VisibilityBookObject BookObject;

    public void ObjectFound(Vector3 dest, Vector3 obj, string objectId) {
        lookingForType = "";
        allowDynamicStates = false;
        GetComponent<NavMeshNavigator>().ObjectFound(dest, obj, objectId);
    }

    private void RandomObjective() {
        if (visibleObjects.Count > 0 && Random.Range(0.0f, 1.0f) < 0.05f) {
            // escollir un objecte visible i cridar al taskmanager perque creei un estat
            //GetComponent<TaskManager>()
            Debug.Log("DynamicState");
        }
    }

    IEnumerator FindVisibleObjects() {
        while (true) {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < spheres.Count; i++) //DEBUG
                Destroy(spheres[i]);
            spheres = new List<GameObject>();
            
            if (allowDynamicStates) {
                VisibleObjects();
                if (lookingForType != "") {
                    List<string> aux = new List<string>();
                    foreach (GameObject obj in visibleObjects) { //POTSER CANVIAR LA FORMA DE ESCOLLIR A RANDOM
                        if (obj.name.Split('_')[0] == lookingForType)
                            aux.Add(obj.name);
                    }
                    if (aux.Count > 0) BookObject.Invoke(this, lookingForType, aux);
                    else RandomObjective();
                }
                else
                    RandomObjective();
            }
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
                if (Physics.Raycast(auxPosition, directionToObject, out hit, viewDistance, 0b1000_1111_0000_0001)) {
                    //Debug.Log("Hitting " + hit.collider.name);
                    if (hit.collider == posibleVisibleObjects[i]) {
                        //Debug.Log("Seeing " + posibleVisibleObjects[i].name);
                        visibleObjects.Add(hit.collider.gameObject);

                        Vector3 aux = objectPosition;//DEBUG
                        aux.y = 2.5f;
                        spheres.Add(Instantiate(sphere, aux, new Quaternion()));
                    }
                }
            }
        }
    }

    void Start() 
    {
        StartCoroutine("FindVisibleObjects");
    }

}
