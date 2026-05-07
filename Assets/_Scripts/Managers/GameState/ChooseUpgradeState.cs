using UnityEngine;

public class ChooseUpgradeState : IGameState
{
    private GameManager _gameManager;

    public ChooseUpgradeState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering ChooseUpgradeState");
        _gameManager.PauseGame(true);
        InputManager.instance.DisableInput();
        _gameManager.StartUpgrade();
    }
    public void Exit() { Debug.Log("Exiting ChooseUpgradeState"); }
    public void Update() { }
}