using UnityEngine;

public class PlayingState : IGameState
{
    private GameManager _gameManager;

    public PlayingState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering PlayingState");
        InputManager.instance.EnableInput();
        _gameManager.PauseGame(false);
    }
    public void Exit()
    { Debug.Log("Exiting PlayingState"); _gameManager.PauseGame(true); InputManager.instance.DisableInput(); }
    public void Update() { }
}