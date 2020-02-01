using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMDetectionManager : MonoBehaviour
{
    private List<float> taps;
    private float meanDelta;

    public int accuracy;
    public float BPM;

    // Start is called before the first frame update
    void Start()
    {
        taps = new List<float>();
        Debug.Log(accuracy);
        meanDelta = 0;
        BPM = 0;
    }
    public void tap() {
        float currentTap = Time.fixedTime;

        if (taps.Count > 0) {
            float lastTap = taps[taps.Count - 1];
           
            float delta = currentTap - lastTap;

            int numberOfDeltas = taps.Count - 1; 

            float tolerance = meanDelta * ((100 - accuracy) / 100);

            meanDelta = (meanDelta * numberOfDeltas + delta) / (numberOfDeltas + 1); 

            if (delta > meanDelta + tolerance || delta < meanDelta - tolerance) {
                Reset();
                meanDelta = delta;
                Debug.Log("reset");
            }

            BPM = 60/meanDelta;
            Debug.Log(BPM);
        }

        taps.Add(currentTap);
    }

    void Reset() {
        this.taps = new List<float>();
    }
}
