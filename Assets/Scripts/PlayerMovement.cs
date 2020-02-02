using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 1f;
    
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
    public GameObject gameOver;

    public List<float> SpeedHistory { get => speedHistory; set => speedHistory = value; }

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
            gameOver.SetActive(true);
            StartCoroutine(Countdown());

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
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{

        //    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        //    isGrounded = false;
       // }
        //else
       // {
        //    rb.AddForce(transform.up * -20000);
       // }
    }
   private IEnumerator Countdown()
         {
           yield return new WaitForSeconds(2f);

            SceneManager.LoadScene(0);
        }
    
}
