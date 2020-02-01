using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMManager : MonoBehaviour
{
    private static BPMManager BPMManagerInstance;
    public float bPM;
    private float beatInterval, beatTimer, beatIntervalD16, beatTimerD16;
    public static bool beatFull, beatD16;
    public static int beatCountFull, beatCountD16;

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
        beatFull = false;
        beatInterval = 60 / bPM;
        beatTimer += Time.deltaTime;

        if (beatTimer >= beatInterval) {
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;
            Debug.Log("Full");

        }

        //16tel
        beatD16 = false;
        beatIntervalD16 = beatInterval / 16;
        beatTimerD16 += Time.deltaTime;

        if (beatTimerD16 >= beatIntervalD16) {
            beatTimerD16 -= beatIntervalD16;
            beatD16 = true;
            beatCountD16++;
            Debug.Log("D16");
        }
    }
}
