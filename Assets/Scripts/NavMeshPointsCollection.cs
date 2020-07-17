using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshPointsCollection : MonoBehaviour
{

	// This Singleton class holds all the NavMeshPoints in the scene, so that any NavMeshNavigator gets whichever they want on instantiating

	public static NavMeshPointsCollection instance;
	private List<NavMeshPoints> collection;

	// Function used to get a specific NavMeshPoints
	public NavMeshPoints GetPoints(int i) {
		return collection[i];
	}

	// Function used to add a NavMeshPoints to the collection
	public void AddPoints(NavMeshPoints points) {
		collection.Add(points);
	}

    // Singleton pattern
    void Awake()
    {
		if (NavMeshPointsCollection.instance) Destroy(this);
		NavMeshPointsCollection.instance=this;
	}
}
