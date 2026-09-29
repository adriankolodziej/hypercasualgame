using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI counterText;

    public void SetCounterText(int count, int maxCount, Color counterColor)
    {
        if(count != maxCount)
        {
            counterText.SetText($"{count}/{maxCount}");
        }
        else
        {
            counterText.SetText("MAX");
        }
        counterText.color = counterColor;
    }
}
