using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	public static AudioPlayer instance;
	public float repeatableWait;

	void Start() {
		if (AudioPlayer.instance) Destroy(this);
		AudioPlayer.instance=this;
		StartCoroutine(instance.PlayRepeatableAnnouncement(1));
	}

	IEnumerator PlayRepeatableAnnouncement(int id) {
		AudioSource[] audios = GetComponents<AudioSource>();
		yield return new WaitForSeconds(repeatableWait+audios[id].clip.length);
		instance.PlayAudio(id, 0);
		StartCoroutine(instance.PlayRepeatableAnnouncement(id));
	}

	public void PlayAudio(int id, float delay) {
		AudioSource[] audios = GetComponents<AudioSource>();
		audios[id].PlayDelayed(delay);
	}
}
