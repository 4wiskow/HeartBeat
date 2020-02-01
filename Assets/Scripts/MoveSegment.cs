using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveSegment : MonoBehaviour
{
    public float speed = -1;
    public GameObject segment, rampSegment, bridgeSegment, obstacle;
    private List<GameObject> segments = new List<GameObject>();
    public int nSegments = 10, killzoneBehindCamera = 10;
    private float distanceTravelled, speedMultiplier = 1f;
    public int rampOffSet;
    List<string> SegsDistribution;


    // Start is called before the first frame update
    void Start()
    {
        SegsDistribution = Enumerable.Repeat("normal", (int) Math.Floor(0.8 * nSegments)).ToList();
        List<string> rampSegs = Enumerable.Repeat("ramp", (int)Math.Floor(0.1 * nSegments)).ToList();
        List<string> bridgeSegs = Enumerable.Repeat("bridge", (int)Math.Floor(0.1 * nSegments)).ToList();
        SegsDistribution.AddRange(rampSegs);
        SegsDistribution.AddRange(bridgeSegs);

        GameObject latestSegment = segment;
        segments.Add(Instantiate(segment));
        for (int i = 0; i<nSegments; i++)
        {
            latestSegment = createNewSegment(latestSegment);
            segments.Add(latestSegment);
        }

    }

    private GameObject createNewSegment(GameObject latestSegment)
    {
        float segmentSize = latestSegment.transform.localScale.z;
        int idx = UnityEngine.Random.Range(0, SegsDistribution.Count);
        string newSegmentID = SegsDistribution[idx];
        Vector3 position;
        newSegmentID = latestSegment.tag == "special" ? "normal" : newSegmentID;
        switch(newSegmentID)
        {
            case "ramp":
                position = new Vector3(0, 0, (segmentSize / 2) * 10 + latestSegment.transform.position.z);
                latestSegment = Instantiate(rampSegment, position, Quaternion.Euler(-20f, 0f, 0f));
                break;
            case "bridge":
                position = new Vector3(0, 0, segmentSize * 10 + latestSegment.transform.position.z);
                Debug.Log(position);
                latestSegment = Instantiate(bridgeSegment, position, Quaternion.identity);
                break;
            default:
                segmentSize = latestSegment.tag != "special" ? segmentSize : segmentSize + rampOffSet; //add space after ramp
                position = new Vector3(0, 0, segmentSize * 10 + latestSegment.transform.position.z);
                latestSegment = Instantiate(segment, position, Quaternion.identity);

                obstacleRandomSpawn(latestSegment);
                break;

        }
        return latestSegment;

    }

    private void obstacleRandomSpawn(GameObject latestSegment)
    {
        int rand = Random.Range(0, 100);
        if (rand < 20)
        {
            Vector3 floorPos = latestSegment.transform.position;
            Vector3 floorScale = latestSegment.transform.localScale;
            float xPos = Random.Range(-(floorScale.x / 2), (floorScale.x / 2));
            Vector3 obstaclePos = floorPos + new Vector3(xPos, 0f, 0f);
            GameObject newObstacle = Instantiate(obstacle, obstaclePos, Quaternion.identity);
            newObstacle.transform.parent = latestSegment.transform;
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
        float moveBy = speedMultiplier * speed * Time.deltaTime;
        foreach (GameObject segment in segments)
        {
            if ((segment.transform.position.z + moveBy) >= Camera.main.transform.position.z - killzoneBehindCamera)
            {
                segment.transform.Translate(new Vector3(0f, 0f, moveBy), Space.World);
            }
            else
            {
                toDestroy = segment;
            }
        }
        distanceTravelled += moveBy;
        Debug.Log(distanceTravelled);

        if (!(toDestroy == null))
        {
            float lastSegmentZpos = segments[nSegments - 1].transform.position.z;
            segments.Remove(toDestroy);

            float segmentSize = segment.transform.localScale.z;
            segments.Add(createNewSegment(segments[nSegments - 1]));

            Destroy(toDestroy);
            toDestroy = null;
        }
    }
}
