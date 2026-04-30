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
        _gameManager.EndLevel(); // Cleanup any previous game state
    }
    public void Exit() { Debug.Log("Exiting MainMenuState"); }
    public void Update() { } // Main menu usually doesn't need per-frame updates
}