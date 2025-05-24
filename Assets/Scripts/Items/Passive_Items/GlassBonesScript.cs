using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Items/GlassBones")]
public class GlassBonesScript : TriggeredItemsScript
{
    private CombatManager m_CombatManager = null;

    public float m_EnemyHealthReduction = 0.9f;

    public override void OnTriggerActivated()
    {
        base.OnTriggerActivated();

        m_CombatManager = FindAnyObjectByType<CombatManager>();

        //-10% to all non boss enemies
        foreach (EnemyBaseScript enemy in m_CombatManager.m_CombatEnemies)
        {
            if (!enemy.m_IsBoss)
            {
                enemy.m_HealthController.m_CurrentHealthPoints *= m_EnemyHealthReduction;
            }
        }
    }
}
