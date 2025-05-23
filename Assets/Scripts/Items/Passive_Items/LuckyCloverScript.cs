using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Items/LuckyClover")]
public class LuckyCloverScript : TriggeredItemsScript
{
    public float m_PercentageOfHealthRevivedWith = 0.5f;
    PlayerHealthController m_PlayerHealthController = null;

    public override void OnTriggerActivated()
    {
        base.OnTriggerActivated();
        m_PlayerHealthController = FindAnyObjectByType<PlayerHealthController>();
        m_PlayerHealthController.m_HealthPoints = m_PlayerHealthController.m_MaxHealthPoints * m_PercentageOfHealthRevivedWith;
    }
}
