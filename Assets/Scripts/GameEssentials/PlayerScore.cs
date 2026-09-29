using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private float scoreOverTimeDecreaseFactor = 0.02f;
    [SerializeField]
    private float scoreOverTimeDecreaseInterval = 2f;

    private float scoreTimer;
    private float currentScoreModifier;
    private float currentScore;
    private float overallScore;
    private float levelScore;

    public float CurrentScore => currentScore;
    public float LevelScore => levelScore;

    private void Start()
    {
        scoreText.SetText($"Your overall score: { PlayerPrefs.GetFloat("Score")}");
        overallScore = PlayerPrefs.GetFloat("Score");
    }

    public void ResetModifier()
    {
        levelScore = 0;
        currentScoreModifier = 0;
    }

    private void Update()
    {
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= scoreOverTimeDecreaseInterval)
        {
            scoreTimer = 0;
            currentScoreModifier += scoreOverTimeDecreaseFactor;
        }
    }

    public void AddScore(BuildingOwner owner)
    {
        if (owner is PlayerController)
        {
            currentScore += 1 - 1 * currentScoreModifier;
            levelScore += 1 - 1 * currentScoreModifier;
        }
        else
        {
            currentScore -= 1 + 1 * currentScoreModifier;
            levelScore -= 1 + 1 * currentScoreModifier;
        }
        if (currentScore < 0)
        {
            currentScore = 0;
            levelScore = 0;
        }

        PlayerPrefs.SetFloat("Score", overallScore + CurrentScore);
    }
}
