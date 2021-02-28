using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneLife : MonoBehaviour
{

    public float appear = 1f;
    public float life = 37.07f;

    private MeshRenderer mesh;

    void CreatePhone() {
        mesh.enabled = true;
    }

    void DeletePhone() {
        Destroy(gameObject);
    }

    public void Invokes(float timeBeforeAppering, float lifeTime) {
        Invoke("CreatePhone", appear);
        Invoke("DeletePhone", life);
    }

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;

        Invokes(0, 0);
    }

}
