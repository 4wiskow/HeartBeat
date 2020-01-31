using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMManager : MonoBehaviour
{
    private static BPMManager BPMManagerInstance;
    public float bPM;
    private float beatInterval, beatTimer, beatIntervalD8, beatTimerD8;
    public static bool beatFull, beatD8;
    public static int beatCountFull, beatCountD8;

    private void Awake() {
        if (BPMManagerInstance != null && BPMManagerInstance != this) {
            Destroy(this.gameObject);
        } else {
            BPMManagerInstance = this;
            DontDestroyOnLoad(this.gameObject)
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BeatDetection() {
        //Takte
        beatFull = false;
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;

        if (beatTimer >= beatInterval {
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;
            Debug.Log("Full");

        }

        //8tel
        beatD8 = false;
        beatIntervalD8 = beatInterval / 8;
        beatTimerD8 += Time.deltaTime;

        if (beatTimerD8 >= beatIntervalD8 {
            beatTimerD8 -= beatIntervalD8;
            beatD8 = true;
            beatCountD8++;
            Debug.Log("D8");
        }
    }
}
