using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    Text scoreText;
    public static int scoreValue = 0;
	// Use this for initialization
	void Start ()
    {
        scoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = "Score: " + scoreValue;
	}
}
