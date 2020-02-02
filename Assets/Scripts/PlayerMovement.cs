using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 1f;
    public int speedHistoryLength = 20;
    public float jumpForce = 4000;
    private float distanceTravelled = 0;
    private float fallDownKillZone = -100;
    private List<float> speedHistory = new List<float>();
    Rigidbody rb;
    bool isGrounded;
    Vector3 jump;
    GameObject game;
    GameObject camera;
    private bool isBoostReady;
    public float boostSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        game = GameObject.Find("Game");
        camera = GameObject.Find("RunnerCam");
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(transform.position.y < fallDownKillZone || transform.position.z < camera.transform.position.z)
        {
            transform.gameObject.SetActive(false);
        }
        if(isBoostReady && transform.position.z < 0)
        {
            transform.position += new Vector3(0f, 0f, boostSpeed * Time.deltaTime);
        }


    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-horizontalSpeed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(horizontalSpeed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        else
        {
            rb.AddForce(transform.up * -20000);
        }
    }

    public void setBPM(float bpm)
    {
        speedHistory.Add(bpm);
        if(speedHistory.Count > speedHistoryLength)
        {
            speedHistory.RemoveAt(0);
        }
        float sum = 0;
        foreach(float val in speedHistory)
        {
            sum += val;
        }
        float avg = sum / speedHistory.Count;
        if (bpm < avg * 1.1 || bpm > avg * 0.9)
        {
            isBoostReady = true;
            Debug.Log(isBoostReady);
        }
    }

}
