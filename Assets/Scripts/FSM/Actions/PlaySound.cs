using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlaySound", menuName = "FSM/Action/Play Sound")]
public class PlaySound : FSMaction {

	public int id;
	public float delay;

	public override void Act(FSMcontroller controller) {
		if (!condition || condition.Decide(controller)) AudioPlayer.instance.PlayAudio(id,delay);
	}
}
