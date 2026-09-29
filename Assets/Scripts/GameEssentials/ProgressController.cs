using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    public event Action<BuildingOwner> LevelWon;

    [SerializeField]
    private List<BuildingController> buildings;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private EnemyController enemyController;
    [SerializeField]
    private ProgressUI progressBar;

    private PlayerScore playerScore;
    private int playerCitizens = 0;
    private int enemyCitizens = 0;

    public float LevelScore => playerScore.LevelScore;

    private void Start()
    {
        foreach (var building in buildings)
        {
            building.NewCitizenArrived += OnCitizenArrival;
            if (building.Owner == playerController)
            {
                playerCitizens += building.Stats.StartingCitizensCount;
            }
            else if (building.Owner == enemyController)
            {
                enemyCitizens += building.Stats.StartingCitizensCount;
            }
        }
        playerScore = FindObjectOfType<PlayerScore>();
    }

    private void OnDestroy()
    {
        foreach (var building in buildings)
        {
            building.NewCitizenArrived -= OnCitizenArrival;
        }
    }

    private void OnCitizenArrival(BuildingOwner owner, int amount)
    {
        if (owner == playerController)
        {
            playerCitizens += amount;
            playerScore.AddScore(playerController);
        }
        else if (owner == enemyController)
        {
            enemyCitizens += amount;
            playerScore.AddScore(enemyController);
        }
        float citizensCount = playerCitizens + enemyCitizens;
        float playerCitizensFactor = playerCitizens / citizensCount;
        float enemyCitizensFactor = enemyCitizens / citizensCount;
        if (playerCitizensFactor == 1)
        {
            LevelWon?.Invoke(playerController);
        }
        if (enemyCitizensFactor == 1)
        {
            LevelWon?.Invoke(enemyController);
        }
        progressBar.SetBarValues(playerCitizensFactor, enemyCitizensFactor);
        progressBar.SetScoreText(playerScore.CurrentScore);

    }

#if UNITY_EDITOR

    [ContextMenu("Find all buildings")]
    private void FindAllBuildings()
    {
        buildings = FindObjectsOfType<BuildingController>().ToList();
        EditorUtility.SetDirty(gameObject);
    }
#endif
}
