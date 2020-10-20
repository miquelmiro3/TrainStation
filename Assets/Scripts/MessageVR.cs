using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageVR : MonoBehaviour
{
	public static MessageVR instance;
	public float time;
	private float timer;
	public string message;
	public bool printing;
	private TMP_Text t;

	public static void PrintMessage(string m, float duration) {
		MessageVR.instance.ShowMessage(m, duration);
	}

	public void ShowMessage(string m, float duration) {
		message=m;
		if (duration>0) time=duration;
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
		t=GetComponentInChildren<TMP_Text>();
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
