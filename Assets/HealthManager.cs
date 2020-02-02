using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 100;
    private GameObject player;
    private List<float> speedHistory = new List<float>();
    public int speedHistoryLength = 20;
    public BPMManager BPMManager;
    public Text text;

    public GameObject gameOver;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        player = GameObject.Find("Player");
        speedHistory = player.GetComponent<PlayerMovement>().SpeedHistory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBPM(float bpm)
    {
        speedHistory.Add(bpm);
        if (speedHistory.Count > speedHistoryLength)
        {
            speedHistory.RemoveAt(0);
        }
        float sum = 0;
        foreach (float val in speedHistory)
        {
            sum += val;
        }
        float avg = sum / speedHistory.Count;
        if (bpm > 50 && bpm < 200)
        {
            if(bpm < avg * 1.1 && bpm > avg * .9)
            {
                health++;
            }
        }
        else
        {
            health -= 2;
        }

        text.text = health.ToString();

        if(health < 1)
        {
            Debug.Log("Game Over");
            GameOver();
        }
    }

    private void GameOver()
    {
        BPMManager.Phase();
        gameOver.SetActive(true);
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(2f);


        SceneManager.LoadScene(0);
    }
}
