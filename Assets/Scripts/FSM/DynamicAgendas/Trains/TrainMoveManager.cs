using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMoveManager : MonoBehaviour
{

    public bool enter = false;
    public bool exit = false;
    public bool countdown = false;
    public float speed = 0f;
    public static float minSpeed = 1f;
    public static float maxSpeed = 100f;
    public static float acceleration = 8f;

    private Vector3 position;
    private float xEndPosition = -155f;
    private Vector3 xVector = new Vector3(1f, 0f, 0f);

    private void EndCountdown() {
        countdown = false;
    }

    public void StartCountdown(float time) {
        countdown = true;
        Invoke("EndCountdown", time);
    }

    void Start()
    {
        position = transform.position;
        speed = minSpeed;
    }

    void Update() 
    {
        if (exit || enter) {
            if ((exit && transform.position.x > xEndPosition) || (enter && transform.position != position)) {
                float direction = exit ? -1f : 1;
                if (direction == -1 && speed < maxSpeed) {
                    speed = speed + acceleration * Time.deltaTime;
                    if (speed > maxSpeed) speed = maxSpeed;
                }
                else if (direction == 1 && speed > minSpeed) {
                    speed = speed - acceleration * Time.deltaTime;
                    if (speed < minSpeed) speed = minSpeed;
                }
                
                if (direction == 1 && Vector3.Distance(transform.position, position) <= 0.05f)
                    transform.position = position;
                else
                    transform.position = transform.position + direction * xVector * speed * Time.deltaTime;
            }
            else {
                exit = false;
                enter = false;
            }
        }
    }
}
