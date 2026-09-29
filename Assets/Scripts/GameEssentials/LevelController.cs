using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static event Action<bool> UIToggled;

    private ProgressController progressController;
    private PlayerController playerController;
    private EnemyController enemyController;
    private LevelEndUIController levelEndUI;

    public void OnLevelLoaded()
    {
        if (progressController != null)
        {
            progressController.LevelWon -= OnLevelWon;
        }
        progressController = FindObjectOfType<ProgressController>();
        playerController = FindObjectOfType<PlayerController>();
        enemyController = FindObjectOfType<EnemyController>();
        levelEndUI = FindObjectOfType<LevelEndUIController>(true);
        if (progressController != null)
        {
            progressController.LevelWon += OnLevelWon;
        }
    }

    private void OnLevelWon(BuildingOwner winner)
    {

        if (winner == playerController)
        {
            levelEndUI.ShowWinUI(progressController.LevelScore);
        }
        else if (winner == enemyController)
        {
            levelEndUI.ShowLoseUI(progressController.LevelScore);
        }
        SubscribeButtons(winner == playerController);
        UIToggled?.Invoke(true);
    }

    private void SubscribeButtons(bool hasPlayerWon)
    {
        levelEndUI.MenuButton.onClick.AddListener(LevelsManager.Instance.GoBackToMenu);
        if (hasPlayerWon)
        {
            levelEndUI.LevelButton.onClick.AddListener(LevelsManager.Instance.PlayNextLevel);
        }
        else
        {
            levelEndUI.LevelButton.onClick.AddListener(LevelsManager.Instance.RestartCurrentLevel);
        }
    }
}
