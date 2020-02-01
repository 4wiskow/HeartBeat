using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 100f;
    public float jumpForce = 4000;
    private float distanceTravelled = 0;
    private float fallDownKillZone = -100;
    Rigidbody rb;
    bool isGrounded;
    Vector3 jump;
    GameObject game;
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        game = GameObject.Find("Game");
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(transform.position.y < fallDownKillZone)
        {
            transform.gameObject.SetActive(false);
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
}
