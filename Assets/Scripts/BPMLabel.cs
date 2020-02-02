using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BPMLabel : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.SetText("0 BPM");
    }

    public void setBPM(int bpm)
    {
        textMeshPro.SetText(bpm + " BPM");
    }
}
