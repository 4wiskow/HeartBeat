using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BPMDetectionManager : MonoBehaviour
{
    private List<float> taps;
    private float meanDelta;
    private GameObject player;

    private GameObject game;
    public float accuracy;
    public float BPM;

    // Start is called before the first frame update
    void Start()
    {
        taps = new List<float>();
        meanDelta = 0;
        BPM = 0;
        player = GameObject.Find("Player");
        game = GameObject.Find("Game");
    }
    public void tap() {
        float currentTap = Time.fixedTime;

        if (taps.Count > 0) {
            float lastTap = taps[taps.Count - 1];
           
            float delta = currentTap - lastTap;

            int numberOfDeltas = taps.Count - 1; 

            float tolerance = meanDelta * (1.0f - accuracy);
            Debug.Log("MeanDelta: " + meanDelta);
            Debug.Log("Tolerance: " + tolerance);
            Debug.Log("Delta: " + delta);

            if (delta > meanDelta + tolerance || delta < meanDelta - tolerance) {
                Reset();
                meanDelta = delta;
                Debug.Log("reset");
            } else {
                meanDelta = (meanDelta * numberOfDeltas + delta) / (numberOfDeltas + 1); 
            }

            BPM = 60/meanDelta;
            Debug.Log(BPM);
            game.GetComponent<MoveSegment>().setSpeed(BPM);
            player.GetComponent<PlayerMovement>().setBPM(BPM);
        }

        taps.Add(currentTap);
    }

    void Reset() {
        this.taps = new List<float>();
    }

 
}
