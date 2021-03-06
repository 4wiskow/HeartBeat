﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class MoveSegment : MonoBehaviour
{
    public float speed = 0;
    public GameObject segment, rampSegment, bridgeSegment, obstacle, slowpassSegment;
    public List<GameObject> obstacles = new List<GameObject>();
    public Camera camera;
    private List<GameObject> segments = new List<GameObject>();
    public int nSegments = 100, killzoneBehindCamera = 1;
    private float distanceTravelled, speedMultiplier = 1f;
    public int rampOffSet = 10;
    List<string> SegsDistribution;
    private float xBorderPath = 3;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        SegsDistribution = Enumerable.Repeat("normal", (int) Math.Floor(0.7 * nSegments)).ToList();
        List<string> rampSegs = Enumerable.Repeat("ramp", (int)Math.Floor(0.1 * nSegments)).ToList();
        List<string> bridgeSegs = Enumerable.Repeat("bridge", (int)Math.Floor(0.1 * nSegments)).ToList();
        List<string> slowpassSegs = Enumerable.Repeat("slowpass", (int)Math.Floor(0.1 * nSegments)).ToList();
        SegsDistribution.AddRange(rampSegs);
        SegsDistribution.AddRange(bridgeSegs);
        SegsDistribution.AddRange(slowpassSegs);

        GameObject latestSegment = segment;
        segments.Add(Instantiate(segment, new Vector3(0f, 0f, 0f), Quaternion.identity));
        for (int i = 0; i<nSegments; i++)
        {
            latestSegment = createNewSegment(latestSegment);
            segments.Add(latestSegment);
        }

    }

    private GameObject createNewSegment(GameObject latestSegment)
    {
        float segmentSize = latestSegment.GetComponent<Renderer>().bounds.size.z;
        int idx = UnityEngine.Random.Range(0, SegsDistribution.Count);
        string newSegmentID = SegsDistribution[idx];
        Vector3 position;
        newSegmentID = latestSegment.tag == "special" ? "normal" : newSegmentID;
        switch(newSegmentID)
        {
            case "ramp":
                float rampSize = rampSegment.GetComponent<Renderer>().bounds.size.z;
                position = new Vector3(0, 0, segmentSize / 2 + latestSegment.transform.position.z);
                latestSegment = Instantiate(rampSegment, position, Quaternion.Euler(-20f, 0f, 0f));
                break;
            case "bridge":
                Vector3 bridgeSize = bridgeSegment.GetComponent<Renderer>().bounds.size;
                float randomBridgeX = Random.Range(-(xBorderPath / 2) + bridgeSize.x / 2, (xBorderPath / 2) - bridgeSize.x / 2);
                position = new Vector3(randomBridgeX, -0.18f, bridgeSize.z / 2 + segmentSize / 2 + latestSegment.transform.position.z);
                latestSegment = Instantiate(bridgeSegment, position, Quaternion.Euler(-90f, 0f, 0f));
                break;
            case "slowpass":
                Vector3 slowpassSize = slowpassSegment.GetComponent<Renderer>().bounds.size;
                //float randomSlowpassX = Random.Range(-(xBorderPath / 2) + slowpassSize.x / 2, (xBorderPath / 2) - slowpassSize.x / 2);
                position = new Vector3(0f, -0.18f, slowpassSize.z / 2 + segmentSize / 2 + latestSegment.transform.position.z);
                latestSegment = Instantiate(slowpassSegment, position, Quaternion.Euler(90f, 0f, 0f));
                break;
            default:
                float normalSegmentSize = segment.GetComponent<Renderer>().bounds.size.z;
                position = new Vector3(0, 0, normalSegmentSize / 2 + segmentSize / 2 + latestSegment.transform.position.z);
                if(latestSegment.name == "Plane(Clone)")
                {
                    position += new Vector3(0f, 0f, rampOffSet);
                }
                latestSegment = Instantiate(segment, position, Quaternion.identity);

                obstacleRandomSpawn(latestSegment);
                break;

        }
        return latestSegment;

    }

    private void obstacleRandomSpawn(GameObject latestSegment)
    {
        int rand = Random.Range(0, 100);
        if (rand < 80)
        {
            int randIdx = Random.Range(0, obstacles.Count);
            obstacle = obstacles[randIdx];
            float obstacleSize = obstacle.GetComponent<Renderer>().bounds.size.x;

            Vector3 floorPos = latestSegment.transform.position;
            float floorScaleX = latestSegment.GetComponent<Renderer>().bounds.size.x;
            float floorScaleZ = latestSegment.GetComponent<Renderer>().bounds.size.z;
            float xPos = Random.Range(-(floorScaleX / 2)+ obstacleSize / 2, (floorScaleX / 2)- obstacleSize / 2);
            float zPos = Random.Range(-(floorScaleZ / 2) + obstacleSize / 2, (floorScaleZ / 2) - obstacleSize / 2);
            Vector3 obstaclePos = floorPos + new Vector3(xPos, 1f, zPos);

            GameObject newObstacle = Instantiate(obstacle, obstaclePos, Quaternion.Euler(90f, 0f, 0f));
            newObstacle.transform.parent = latestSegment.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveAllSegments();
        text.text = ((int) -distanceTravelled).ToString();
    }

    private void moveAllSegments()
    {
        GameObject toDestroy = null;
        float moveBy = speedMultiplier * speed * Time.deltaTime;
        foreach (GameObject segment in segments)
        {
            if ((segment.transform.position.z + moveBy) >= camera.transform.position.z - killzoneBehindCamera)
            {
                segment.transform.Translate(new Vector3(0f, 0f, moveBy), Space.World);
            }
            else
            {
                toDestroy = segment;
            }
        }
        distanceTravelled += moveBy;

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

    public void setSpeed (float speedBPM)
    {
        speed = -Math.Min(speedBPM/10, 40);
    }
}
