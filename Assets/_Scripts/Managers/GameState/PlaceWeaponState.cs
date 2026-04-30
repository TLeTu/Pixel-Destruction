using UnityEngine;

public class PlaceWeaponState : IGameState
{
    private GameManager _gameManager;

    public PlaceWeaponState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering PlaceWeaponState");
        _gameManager.PauseGame(true); // Pause game
        InputManager.instance.EnableInput(); // Enable input for placing weapons
    }
    public void Exit() { Debug.Log("Exiting PlaceWeaponState"); }
    public void Update() { }
}