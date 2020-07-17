using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshPoints : MonoBehaviour
{

	public List<Vector3> points;

	public Vector3 GetRandomPoint() {
		int i = Random.Range(0, points.Count);
		return points[i];
	}

    // Start is called before the first frame update
    void Start()
    {
		foreach (Transform child in transform) {
			points.Add(child.position);
		}
	}
}
