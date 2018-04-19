using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float jumpForce = 200.0f;
    public float runningSpeed = 3.0f;
    private Rigidbody2D rigidBody;
    public float fartDuration = 3.0f;
    private float fartTimer;
    public bool IsFarting;
    // Use this for initialization
    private void Awake()
    {
 
        rigidBody = GetComponent<Rigidbody2D>();
        IsFarting = false;
    }
 //   void Start () {
        
    //}

    
    // Update is called once per frame
    void Update ()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (Input.GetButtonDown("Fart"))
            {
                //Commence farting
                if (!IsFarting)
                {
                    IsFarting = true;
                    fartTimer = fartDuration;
                    //TODO: PLAY FARTING SOUND
                }
            }

            //While is farting, spawn particle effects at the player's current position
            if (IsFarting)
            {
                Fart();
            }

        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (Input.GetButtonDown("w"))
            {
                Jump();
            }
            if (Input.GetButton("d"))
            {
                if (rigidBody.velocity.x < runningSpeed)
                {
                  
                    WalkRight();
                }
            }
            if (Input.GetButton("a"))
            {
                if (rigidBody.velocity.x > -runningSpeed)
                {
                    
                    WalkLeft();
                }
            }
            if (Input.GetButtonDown("m"))
            {
                Debug.Log("Menu...");
                GameManager.instance.SetGameState(GameState.menu);
            }
            
        }
    }

    void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    void WalkRight()
    {
        rigidBody.AddForce(Vector2.right * runningSpeed, ForceMode2D.Impulse);
    }
    void WalkLeft()
    {
        rigidBody.AddForce(Vector2.left * runningSpeed, ForceMode2D.Impulse);
    }
    void Crouch()
    {

    }

    void Fart()
    {
        fartTimer -= Time.deltaTime;
        //TODO: SPAWN PARTICLE EFFECTS
        Debug.Log("Releasing FART!");

        if(fartTimer < 0)
        {
            Debug.Log("Finish farting");
            IsFarting = false;
        }
    }
}
