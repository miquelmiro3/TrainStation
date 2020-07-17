using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshPoints : MonoBehaviour
{

	public List<Vector3> points;

	// Returns a random point from all the ones that this NavMeshPoints holds.
	public Vector3 GetRandomPoint() {
		int i = Random.Range(0, points.Count);
		return points[i];
	}

    // On Start, the script stores the positions of all the children of this gameobject as possible points to navigate to for the NavMeshNavigator.
    void Start()
    {
		foreach (Transform child in transform) {
			points.Add(child.position);
		}

		NavMeshPointsCollection.instance.AddPoints(this);
	}
}
