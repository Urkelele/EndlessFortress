using UnityEngine;

public class SummonMeteorFunctionality : BaseActiveScript
{
    public float m_MaxHealthDamagePercentage = 0.4f;
    public float m_BossDamageReduccion = 0.5f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { UseActive(); }
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;

        PlayerHealthController playerHealthController = GetComponent<PlayerHealthController>();

        foreach (EnemyBaseScript enemyScript in CombatManager.instance.m_CombatEnemies)
        {
            // Check if the enemy is a boss if so the damage is half, since its percentage damage do the damage manually and apply lifesteal manually too
            if (CombatManager.instance.m_CurrentEnemyTarget.m_IsBoss)
            {
                enemyScript.gameObject.GetComponent<HealthController>().ReceiveDamage(enemyScript.m_HealthController.m_MaxHealthPoints * m_MaxHealthDamagePercentage * m_BossDamageReduccion);
                playerHealthController.HealDamage(enemyScript.m_HealthController.m_MaxHealthPoints * m_MaxHealthDamagePercentage * m_BossDamageReduccion * InventoryManager.instance.m_TotalLifeSteal);
            }
            else
            {
                enemyScript.gameObject.GetComponent<HealthController>().ReceiveDamage(enemyScript.m_HealthController.m_MaxHealthPoints * m_MaxHealthDamagePercentage);
                playerHealthController.HealDamage(enemyScript.m_HealthController.m_MaxHealthPoints * m_MaxHealthDamagePercentage * InventoryManager.instance.m_TotalLifeSteal);
            }


        }
        return true;
    }
}
