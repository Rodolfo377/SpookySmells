using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	public float walkingSpeed = 1.0f;
	private Rigidbody2D aiRigidBody;
    //FieldOfView myScript;
    //Collider2D player;

    // Use this for initialization
    void Awake ()
	{
		aiRigidBody = GetComponent<Rigidbody2D>();
        //myScript = GetComponent<FieldOfView>();
        //player = FindObjectOfType<TrailRendererWithCollider>();
    }
	
	// Update is called once per frame
	void Update ()
	{

        //if(myScript.playerVisible)
        //{
        //    Debug.Log("Game Over!");
            
        //}

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Collider2D>().name == "Fart")
        {
            Debug.Log("What smell is this?");
        }
    }

}
