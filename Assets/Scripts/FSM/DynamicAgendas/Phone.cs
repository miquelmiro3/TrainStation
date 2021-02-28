using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : StateMachineBehaviour
{

    public GameObject phoneModel;
    private GameObject phone;
    private Transform hand;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        hand = animator.transform.Find(
            "Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand"
        );
        phone = Instantiate(phoneModel, hand.TransformPoint(-0.118f, 0.0261f, 0.0099f), hand.rotation);
        phone.transform.Rotate(new Vector3(12.694f, -23.829f, -11.282f), Space.Self);
        phone.transform.parent = hand;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (phone) Destroy(phone);
    }

}
