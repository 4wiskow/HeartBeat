using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnBeat : MonoBehaviour
{
    public SoundManager soundManager;
    public AudioClip kick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BPMManager.beatFull) {
            soundManager.PlaySound(kick, 1);
        }
    }

    public void playKick() {
         soundManager.PlaySound(kick, 1);
    }
}
