using UnityEngine;

public class EntwinedAttackFunctionality : BaseActiveScript
{
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;
        float damage;
        //damage = m_InventoryManager.m_CurrentLightWeapon.ItemScript.m_WeaponDamage + m_InventoryManager.m_CurrentHeavyWeapon.ItemScript.m_WeaponDamage;
        CombatManager.instance.m_CurrentEnemyTarget.m_HealthController.ReceiveDamage(2);
        return true;
    }
}
