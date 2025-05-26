using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;
    public TextMeshProUGUI m_MainMenuHighScoreText;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        m_MainMenuHighScoreText.text = "High Score: " + ExternalDataManager.Instance.m_StoredData.m_HighScore;
    }

    public void UpdateNewHighScore()
    {
        ExternalDataManager.Instance.m_StoredData.m_HighScore = PlayerStats.instance.CalculateScore();
        m_MainMenuHighScoreText.text = "High Score: " + ExternalDataManager.Instance.m_StoredData.m_HighScore;
    }
}
