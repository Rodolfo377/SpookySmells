using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float jumpForce = 200.0f;
    public float runningSpeed = 3.0f;
    private Rigidbody2D rigidBody;
    // Use this for initialization
    private void Awake()
    {
        Debug.Log("Hello!");
        rigidBody = GetComponent<Rigidbody2D>();
    }
 //   void Start () {
        
    //}

    
    // Update is called once per frame
    void Update ()
    {
          


    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("w"))
        {
            Jump();
        }
        if(Input.GetButton("d"))
        {
            if (rigidBody.velocity.x < runningSpeed)
            {
                WalkRight();
            }
        }
        if(Input.GetButton("a"))
        {
            if (rigidBody.velocity.x > -runningSpeed)
            {
                Debug.Log("Pressed A!");
                WalkLeft();
            }
        }
    }

    void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    void WalkRight()
    {
        rigidBody.AddForce(Vector2.right*runningSpeed, ForceMode2D.Impulse);
    }
    void WalkLeft()
    {
        Debug.Log("Walking Left...");
        rigidBody.AddForce(Vector2.left * runningSpeed, ForceMode2D.Impulse);
    }
    void Crouch()
    {

    }
}
