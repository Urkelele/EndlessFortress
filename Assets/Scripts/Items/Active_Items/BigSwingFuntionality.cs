using UnityEngine;

public class BigSwingFunctionality: BaseActiveScript
{
    public float m_DamageMultiplyier = 2;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { UseActive(); }
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;
        
        foreach (EnemyBaseScript enemyScript in CombatManager.instance.m_CombatEnemies)
        {
            enemyScript.gameObject.GetComponent<HealthController>().ReceiveDamage(InventoryManager.instance.m_CurrentHeavyWeapon.m_WeaponDamage * m_DamageMultiplyier);
        }
        return true;
    }
}
