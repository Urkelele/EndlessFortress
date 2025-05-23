using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Items/DefensiveBreach")]
public class DefensiveBreachScript : TriggeredItemsScript
{
    public float m_MultiplyIncomingDamageBy = 1.1f;
    private CombatManager m_CombatManager = null;

    public override void OnTriggerActivated()
    {
        base.OnTriggerActivated();

        m_CombatManager = FindAnyObjectByType<CombatManager>();

        foreach(EnemyBaseScript enemy in m_CombatManager.m_CombatEnemies)
        {
            enemy.m_HealthController.m_IncomingDamageMultiplier *= m_MultiplyIncomingDamageBy;
        }
    }
}
