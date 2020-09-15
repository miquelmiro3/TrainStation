using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
	public static PlayerPosition instance;
	public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
		if (PlayerPosition.instance) Destroy(this);
		PlayerPosition.instance=this;
    }

	void Update() {
		position=transform.position;
	}

}
