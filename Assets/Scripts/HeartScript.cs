using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HeartScript : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction;

    public PlayManager PlayManager;
    public BPMDetectionManager BPMDetectionManager;

    public Animator animator;

    private GameObject player = GameObject.Find("Player");
    private GameObject game = GameObject.Find("Game");

    void OnCollisionEnter (Collision collisionInfo) {
        PlayManager.playKick();
        BPMDetectionManager.tap();
        
        if (collisionInfo.collider.name == "stick_right") {
            hapticAction.Execute(0, 0.1f, 150, 75, SteamVR_Input_Sources.RightHand);
        } else {
            hapticAction.Execute(0, 0.1f, 150, 75, SteamVR_Input_Sources.LeftHand);
        }
    
        animator.SetTrigger("bump");

        player.GetComponent<PlayerMovement>().setBPM(BPMDetectionManager.BPM);
        game.GetComponent<MoveSegment>().setSpeed(BPMDetectionManager.BPM);
    }
}
