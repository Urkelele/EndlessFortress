using UnityEngine;

public class EntwinedAttackFunctionality : ItemFunctionality
{
    CombatManager m_CombatManager;
    InventoryManager m_InventoryManager;
    public void Start()
    {
        m_CombatManager = FindAnyObjectByType<CombatManager>();
        m_InventoryManager = FindAnyObjectByType<InventoryManager>();
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;
        float damage;
        //damage = m_InventoryManager.m_CurrentLightWeapon.ItemScript.m_WeaponDamage + m_InventoryManager.m_CurrentHeavyWeapon.ItemScript.m_WeaponDamage;
        m_CombatManager.m_CurrentEnemyTarget.m_HealthController.ReceiveDamage(2);
        return true;
    }
}
