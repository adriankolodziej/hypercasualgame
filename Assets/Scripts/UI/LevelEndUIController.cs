using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI messageText;
    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Button levelButton;

    public Button LevelButton => levelButton;
    public Button MenuButton => menuButton;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowWinUI(float playerScore)
    {
        gameObject.SetActive(true);
        messageText.SetText("YOU WON!");
        buttonText.SetText("NEXT LEVEL");
        scoreText.SetText($"Level score: {playerScore}");
    }

    public void ShowLoseUI(float playerScore)
    {
        gameObject.SetActive(true);
        messageText.SetText("YOU LOST :/");
        buttonText.SetText("RESTART");
        scoreText.SetText($"Level score: {playerScore}");
    }
}
