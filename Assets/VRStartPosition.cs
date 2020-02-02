using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRStartPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject game = GameObject.Find("Game");
        GameObject Camera = GameObject.Find("Camera");
        game.transform.position = Camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
