using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardController : MonoBehaviour
{
    public GameObject m_DailyRewardPanel;
    public Button m_DailyRewardButton;
    public TextMeshProUGUI m_RewardText;
    public TextMeshProUGUI m_TimeText;
    public int m_RewardCuantity;
    public float rewardCooldownHours = 8f;

    void Start()
    {
        CheckRewardAvailability();
        m_RewardText.text = m_RewardCuantity.ToString();
    }
    void Update()
    {
        if(ExternalDataManager.Instance.m_StoredData.m_LastTimeDailyReward == null) { return; }
        UpdateCountdownUI();
    }
    bool TryGetLastClaimTime(out DateTime lastClaim)
    {
        string raw = ExternalDataManager.Instance.m_StoredData.m_LastTimeDailyReward;

        if (string.IsNullOrEmpty(raw))
        {
            lastClaim = DateTime.MinValue;
            return false;
        }

        return DateTime.TryParse(raw, null, System.Globalization.DateTimeStyles.RoundtripKind, out lastClaim);
    }
    public bool CanClaimReward()
    {
        if (!TryGetLastClaimTime(out DateTime lastClaim))
            return true; // Allow claim if time is invalid (e.g., first time)

        TimeSpan timeSinceLast = DateTime.UtcNow - lastClaim;
        return timeSinceLast.TotalHours >= rewardCooldownHours;
    }

    public void UpdateCountdownUI()
    {
        if (!TryGetLastClaimTime(out DateTime lastClaim))
        {
            m_TimeText.text = "Reward Ready!";
            return;
        }

        TimeSpan timeSinceLast = DateTime.UtcNow - lastClaim;
        TimeSpan cooldown = TimeSpan.FromHours(rewardCooldownHours);
        TimeSpan remaining = cooldown - timeSinceLast;

        if (remaining.TotalSeconds <= 0)
        {
            m_TimeText.text = "Reward Ready!";
        }
        else
        {
            m_TimeText.text = string.Format("Next reward in: {0:D2}h {1:D2}m {2:D2}s",
                remaining.Hours, remaining.Minutes, remaining.Seconds);
        }
    }


    public void ClaimReward()
    {
        if (!CanClaimReward())
        {
            Debug.Log("Reward not ready yet!");
            return;
        }

        // Give player reward here...
        ExternalDataManager.Instance.AddTomes(m_RewardCuantity);

        ExternalDataManager.Instance.m_StoredData.m_LastTimeDailyReward = DateTime.UtcNow.ToString("o");
        Debug.Log("Reward claimed!");
    }

    void CheckRewardAvailability()
    {
        if (CanClaimReward())
        {
            Debug.Log("Reward available!");
        }
        else
        {
            Debug.Log("Come back later.");
        }
    }
}
