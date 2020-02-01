using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSegment : MonoBehaviour
{
    public float speed = 3f;
    public GameObject segment;
    private List<GameObject> segments = new List<GameObject>();
    public int nSegments = 50;
    public int killzoneBehindCamera = 100;


    // Start is called before the first frame update
    void Start()
    {
        float segmentSize = segment.transform.localScale.z;
        for(int i = 0; i<50; i++)
        {
            segments.Add(Instantiate(segment, new Vector3(0, 0, i * segmentSize * 10), Quaternion.identity));
        
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        moveAllSegments();
    }

    private void moveAllSegments()
    {
        GameObject toDestroy = null;
        foreach (GameObject segment in segments)
        {
            if ((segment.transform.position.z + speed) >= Camera.main.transform.position.z - killzoneBehindCamera)
            {

                segment.transform.Translate(new Vector3(0f, 0f, speed));
            }
            else
            {
                toDestroy = segment;
            }
        }

        if (!(toDestroy == null))
        {
            float lastSegmentZpos = segments[nSegments - 1].transform.position.z;
            segments.Remove(toDestroy);

            float segmentSize = segment.transform.localScale.z;
            segments.Add(Instantiate(segment, new Vector3(0, 0, lastSegmentZpos + segmentSize * 10), Quaternion.identity));

            Destroy(toDestroy);
            toDestroy = null;
        }
    }
}
