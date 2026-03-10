using TMPro;
using UnityEngine;

public class ScoreUIonDeath : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = "SCORE: " + PlayerStats.instance.CalculateScore();
    }
}
