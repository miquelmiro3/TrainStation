using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleHandler : MonoBehaviour
{

	public List<GameObject> people;
	public int initialAmount;

    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i<initialAmount; ++i) {
			Instantiate(people[i]);
		}
    }
}
