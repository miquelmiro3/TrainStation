using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IndividualObjectManager : MonoBehaviour
{

    public float rotation = -1;
    public bool disableNMO = false; //BORRAR
    public bool enableQueue = false;
    public int maxQueueLength = 3;

    private bool booked = false;
    private Transform pos;
    private NavMeshObstacle nmo;
    private Queue<NavMeshNavigator> queue = new Queue<NavMeshNavigator>();

    public void SetFree() {
        booked = false;
        if (enableQueue) {
            queue.Dequeue();
            int i = 0;
            foreach (NavMeshNavigator nmn in queue) {
                Vector3 aux = pos.position - pos.forward * 1.5f * i;
                nmn.ObjectFound(gameObject, aux, i, rotation);
                nmn.SetDestinationToObject();
                ++i;
            }
        }
        if (nmo) Invoke("EnableNMO", 1.0f);
    }

    private void EnableNMO() {
        if (!booked) nmo.enabled = true;
    }

    public Tuple<Vector3, int, float> Book(NavMeshNavigator nmn) {
        if ((!enableQueue && booked) || (enableQueue && queue.Count >= maxQueueLength)) {
            return null;
        }
        else {
            booked = true;
            if (nmo) nmo.enabled = false;
            
            if (enableQueue) {
                queue.Enqueue(nmn);
                Vector3 aux = pos.position - pos.forward * 1.5f * (queue.Count-1);
                return new Tuple<Vector3, int, float>(aux, queue.Count-1, rotation);
            }
            return new Tuple<Vector3, int, float>(pos.position, 0, rotation);
        }
    }

    public bool IsFree() {
        if ((!enableQueue && booked) || (enableQueue && queue.Count >= maxQueueLength)) return false;
        return true;
    }

    void Awake()
    {
        pos = transform.Find("Pos");

        if (disableNMO) nmo = GetComponent<NavMeshObstacle>();
    }

}
