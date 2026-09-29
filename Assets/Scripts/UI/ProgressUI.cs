using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    [SerializeField]
    private Image playerProgressBar;
    [SerializeField]
    private Image enemyProgressBar;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void SetBarValues(float playerProgress, float enemyProgress)
    {
        playerProgressBar.fillAmount = playerProgress;
        enemyProgressBar.fillAmount = enemyProgress;
    }

    public void SetScoreText(float score)
    {
        scoreText.SetText($"Current playthrough score: {score}");
    }
}
