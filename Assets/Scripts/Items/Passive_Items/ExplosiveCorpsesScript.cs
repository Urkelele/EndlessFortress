using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Items/ExplosiveCorpses")]
public class ExplosiveCorpsesScript : TriggeredItemsScript
{
    public float m_MaxHPpercentageDamage = 0.2f;
    private CombatManager m_CombatManager = null;

    public override void OnTriggerActivated()
    {
        base.OnTriggerActivated();

        m_CombatManager = FindAnyObjectByType<CombatManager>();

        foreach (EnemyBaseScript enemy in m_CombatManager.m_CombatEnemies)
        {
            if (enemy != null)
            {
                enemy.m_HealthController.ReceiveDamage(enemy.m_HealthController.m_MaxHealthPoints * m_MaxHPpercentageDamage);
            }
        }
    }
}
