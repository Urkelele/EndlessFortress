using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Items/Active_Items/SummonMeteor")]
public class SummonMeteorFunctionality : BaseActiveScript
{
    public float m_MaxHealthDamagePercentage = 0.4f;
    public float m_BossDamageReduccion = 0.5f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { UseActive(); }
    }
    public override void UseActive()
    {
        base.UseActive();

        PlayerHealthController playerHealthController = FindAnyObjectByType<PlayerHealthController>();

        foreach (EnemyBaseScript enemyScript in CombatManager.instance.m_CombatEnemies)
        {
            // Check if the enemy is a boss if so the damage is half, since its percentage damage do the damage manually and apply lifesteal manually too
            if (enemyScript.m_IsBoss)
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
    }
}
