using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageVR : MonoBehaviour
{
	public static MessageVR instance;
	public float time;
	private float timer;
	public string message;
	public bool printing;
	private Text t;

	public static void PrintMessage(string m) {
		MessageVR.instance.ShowMessage(m);
	}

	public void ShowMessage(string m) {
		message=m;
		t.text=message;
		timer=time;
		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}
		printing=true;
	}

    // Start is called before the first frame update
    void Start()
    {
		if (MessageVR.instance) Destroy(this);
		MessageVR.instance=this;
		t=GetComponentInChildren<Text>();
		timer=0;
		printing=true;
    }

    // Update is called once per frame
    void Update()
    {
		if (printing) {
			timer-=Time.deltaTime;
			if (timer<=0) {
				printing=false;
				foreach (Transform child in transform) {
					child.gameObject.SetActive(false);
				}
			}
		}
    }
}
