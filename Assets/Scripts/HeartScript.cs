﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HeartScript : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction;

    public PlayManager PlayManager;
    public BPMDetectionManager BPMDetectionManager;

    public Animator animator;

    bool collided;
    bool coolidedCP;
    Collider sp;
    
    void OnCollisionEnter (Collision collisionInfo) {
       if(!collided)
       {
           collided = true;
        PlayManager.playKick();
        BPMDetectionManager.tap();
        
        if (collisionInfo.collider.name == "stick_right") {
            hapticAction.Execute(0, 0.1f, 150, 75, SteamVR_Input_Sources.RightHand);
        } else {
            hapticAction.Execute(0, 0.1f, 150, 75, SteamVR_Input_Sources.LeftHand);
        }
       
        animator.SetTrigger("bump");
       }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        collided = false;
    }
}
