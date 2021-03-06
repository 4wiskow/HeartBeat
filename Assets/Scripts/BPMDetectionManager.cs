﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMDetectionManager : MonoBehaviour
{
    private List<float> taps;
    private float meanDelta;

    public float accuracy;
    public float BPM;
    public BPMLabel BPMLabel;
    public BPMManager BPMManager;

    private GameObject player;
    private GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        
        taps = new List<float>();
        game = GameObject.Find("RunGame");
        BPMManager = GameObject.Find("BPMManager").GetComponent<BPMManager>();

    }

    void Update() {
        if (taps.Count > 0) {
            float delta = Time.fixedTime - taps[taps.Count - 1];

            if (delta > 1.0f) {
                BPM = 0;
                BPMLabel.setBPM((int) BPM);
                BPMManager.bPM = (int) BPM;

                game.GetComponent<HealthManager>().setBPM(BPM);
                game.GetComponent<MoveSegment>().setSpeed(BPM);
            }
        }
    }

    public void tap() {
        float currentTap = Time.fixedTime;

        if (taps.Count > 0) {
            float lastTap = taps[taps.Count - 1];
           
            float delta = currentTap - lastTap;

            int numberOfDeltas = taps.Count - 1; 

            float tolerance = meanDelta * (1.0f - accuracy);

            if (delta > meanDelta + tolerance || delta < meanDelta - tolerance) {
                Reset();
                meanDelta = delta;
                Debug.Log("reset");
                BPMManager.Phase();
            } else {
                meanDelta = (meanDelta * numberOfDeltas + delta) / (numberOfDeltas + 1); 
            }

            UpdateBPM();
            game.GetComponent<HealthManager>().setBPM(BPM);
            game.GetComponent<MoveSegment>().setSpeed(BPM);


        }

        taps.Add(currentTap);
    }

    void Reset() {
        this.taps = new List<float>();
    }

    void UpdateBPM()
    {
        BPM = 60 / meanDelta;
        BPMLabel.setBPM((int) BPM);
        BPMManager.bPM = (int) BPM;
    }
}
