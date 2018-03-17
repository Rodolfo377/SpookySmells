using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState;

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
        if(Input.GetButtonDown("g"))
        {
            StartGame();
        }
    }
    void StartGame ()
    {
        Debug.Log("Start Game");
        SetGameState(GameState.inGame);             
    }

    void GameOver ()
    {
        
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

        }

        currentGameState = newgameState;
    }
}
