using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	public static AudioPlayer instance;

	void Start() {
		if (AudioPlayer.instance) Destroy(this);
		AudioPlayer.instance=this;
	}

	public void PlayAudio(int id) {
		AudioSource[] audios = GetComponents<AudioSource>();
		audios[id].Play();
	}
}
