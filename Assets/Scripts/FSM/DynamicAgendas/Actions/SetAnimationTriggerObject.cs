using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetAnimationTriggerObject", menuName = "FSM/Action/Set Animation Trigger Object")]
public class SetAnimationTriggerObject : FSMaction {

	public string[] objects;
	public string[] animations;
	public string defaultAnimation;

	public override void Act(FSMcontroller controller) {
		string tag = controller.GetComponent<NavMeshNavigator>().obj.Item1.tag;
		string anim = defaultAnimation;
		for (int i = 0; i < objects.Length; i++) {
			if (tag == objects[i]) {
				anim = animations[i];
				break;
			}
		}
		controller.GetComponent<Animator>().SetTrigger(anim);
	}

}
