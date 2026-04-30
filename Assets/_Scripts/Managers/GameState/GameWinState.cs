using UnityEngine;

public class GameWinState : IGameState
{
    private GameManager _gameManager;

    public GameWinState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering GameWinState");
        if (SaveManager.instance != null)
        {
            int maxLevelIndex = Mathf.Max(0, _gameManager.LevelConfigs.Count - 1);
            int nextUnlockedLevelIndex = Mathf.Min(_gameManager.currentLevelIndex + 1, maxLevelIndex); // currentLevelIndex needs to be public or passed
            SaveManager.instance.SaveLevelIndex(nextUnlockedLevelIndex);
        }
        AudioManager.instance.PlayLevelWinSFX();
        _gameManager.EndLevel(); // Cleanup current level
        InputManager.instance.DisableInput();
    }
    public void Exit() { Debug.Log("Exiting GameWinState"); }
    public void Update() { }
}