using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkRunScript : MonoBehaviour
{

    public Animation animation;

    public MoveSegment moveSegment;

    void Start() {
       // Debug.Log(animation.Animations);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveSegment.speed == 0f) {
            animation.CrossFade("walk2", 0.3F, PlayMode.StopSameLayer);
            //animation.Stop("walk2");
        } else if (moveSegment.speed < -13.0f) {
            animation.Play("run ");
        } else {
            animation.Play("walk 1");
        }
    }
}
