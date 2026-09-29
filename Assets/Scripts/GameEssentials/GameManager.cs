using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelsManager levelsManager;
    [SerializeField]
    private BuildingSelectionController buildingSelectionController;
    [SerializeField]
    private LevelController levelController;
    [SerializeField]
    private PlayerScore playerScore;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        LevelController.UIToggled += PauseGame;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        LevelController.UIToggled -= PauseGame;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        buildingSelectionController.OnLevelLoaded();
        levelController.OnLevelLoaded();
        playerScore.ResetModifier();
        PauseGame(false);
    }

    private void PauseGame(bool shouldPause)
    {
        if (shouldPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void StartGame()
    {
        levelsManager.StartGame();
        DontDestroyOnLoad(this.gameObject);
    }
}
