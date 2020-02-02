using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMManager : MonoBehaviour
{
    private static BPMManager BPMManagerInstance;
    public float bPM;
    private float quarterInterval, quarterTimer, eighthInterval, eighthTimer;
    public static bool quarter, eighth;
    public static int quarterCount, eighthCount;

    private void Awake() {
        if (BPMManagerInstance != null && BPMManagerInstance != this) {
            Destroy(this.gameObject);
        } else {
            BPMManagerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BeatDetection();
    }

    private void BeatDetection() {
        //Takte
        quarter = false;
        quarterInterval = 60 / bPM;
        quarterTimer += Time.deltaTime;

        if (quarterTimer >= quarterInterval) {
            quarterTimer -= quarterInterval;
            quarter = true;
            quarterCount++;
        }

        //16tel
        eighth = false;
        eighthInterval = quarterInterval / 2;
        eighthTimer += Time.deltaTime;

        if (eighthTimer >= eighthInterval) {
            eighthTimer -= eighthInterval;
            eighth = true;
            eighthCount++;
        }
    }

    public void Phase() {
        quarterTimer = 0;
        eighthTimer = 0;
        eighthCount = 0;
        eighthTimer = 0;
    }
}
