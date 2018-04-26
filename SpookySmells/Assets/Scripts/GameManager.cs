using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver,
    win
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState;
    public bool GhostDiscovered;
    public int numberOfHits;
    //List<AIController> activeNPCs;

    private void Awake()
    {
        instance = this;
       
    }
    void Start()
    {
        Debug.Log("Start");
        currentGameState = GameState.menu;
    }
    private void Update()
    {
        //activeNPCs.Clear();

        GameObject[] NPCs = GameObject.FindGameObjectsWithTag("NPC");

        if(NPCs.Length == 0)
        {
            Debug.Log("YOU WIN");
            SetGameState(GameState.win);
        }
        
        if (Input.GetButtonDown("g"))
        {
            StartGame();
        }
        if(GhostDiscovered)
        {
            SetGameState(GameState.gameOver);
        }

     
       
        
    }
    void StartGame ()
    {
        Debug.Log("Start Game");
        SetGameState(GameState.inGame);             
    }

    void GameOver ()
    {
        Debug.Log("PERDEU PLAYBOY");
    }

    void BackToMenu()
    {
        
    }

    public void SetGameState(GameState newgameState)
    {
        if(newgameState == GameState.menu)
        {

        }
        else if(newgameState == GameState.inGame)
        {

        }
        else if (newgameState == GameState.gameOver)
        {
            GameOver();
        }

        currentGameState = newgameState;
    }
}
