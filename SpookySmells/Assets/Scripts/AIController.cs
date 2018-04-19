using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	public float walkingSpeed = 1.0f;
	private Rigidbody2D aiRigidBody;
    //FieldOfView myScript;
    

    // Use this for initialization
    void Awake ()
	{
		aiRigidBody = GetComponent<Rigidbody2D>();
        //myScript = GetComponent<FieldOfView>();

    }
	
	// Update is called once per frame
	void Update ()
	{

        //if(myScript.playerVisible)
        //{
        //    Debug.Log("Game Over!");
            
        //}
	}
}
