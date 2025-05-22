using UnityEngine;

public class SummonMeteorFunctionality : BaseActiveScript
{
    CombatManager m_CombatManager;

    public float m_MaxHealthDamagePercentage = 0.4f;
    public float m_BossDamageReduccion = 0.5f;

    public void Start()
    {
        m_CombatManager = FindAnyObjectByType<CombatManager>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { UseActive(); }
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;

        foreach (EnemyBaseScript enemyScript in m_CombatManager.m_CombatEnemies)
        {
            // Check if the enemy is a boss if so the damage is half
            if (m_CombatManager.m_CurrentEnemyTarget.m_IsBoss)
            {
                enemyScript.gameObject.GetComponent<HealthController>().ReceiveDamage(enemyScript.m_HealthController.m_MaxHealthPoints * m_MaxHealthDamagePercentage * m_BossDamageReduccion);
            }
            else
            {
                enemyScript.gameObject.GetComponent<HealthController>().ReceiveDamage(enemyScript.m_HealthController.m_MaxHealthPoints * m_MaxHealthDamagePercentage);
            }
        }
        return true;
    }
}
