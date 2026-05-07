using UnityEngine;

public class MainMenuState : IGameState
{
    private GameManager _gameManager;

    public MainMenuState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering MainMenuState");
        _gameManager.EndLevel();
    }
    public void Exit() { Debug.Log("Exiting MainMenuState"); }
    public void Update() { }
}