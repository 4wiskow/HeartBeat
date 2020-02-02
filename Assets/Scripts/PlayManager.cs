using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public SoundManager soundManager;
    public AudioClip kick;
    public AudioClip snare;
    public AudioClip hiHat;

    public static int kickCounter;

    // Update is called once per frame
    void Update()
    {
        // if (BPMManager.quarter) {
        //     soundManager.PlaySound(kick, 0.5f);
        //     Debug.Log("kick");
        // }

        if (BPMManager.quarter && (BPMManager.quarterCount % 2 == 1 )) {
            soundManager.PlaySound(snare, 0.4f);
        }

        if (BPMManager.eighth) {
            soundManager.PlaySound(hiHat, 0.1f);
        }
    }

    public void playKick() {
        soundManager.PlaySound(kick, 0.3f);
        kickCounter++;
    }
}
