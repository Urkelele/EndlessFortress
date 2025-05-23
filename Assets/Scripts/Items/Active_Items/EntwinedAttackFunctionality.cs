using UnityEngine;

public class EntwinedAttackFunctionality : BaseActiveScript
{
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;

        FindAnyObjectByType<PlayerCombatScript>().DealDamageToTargetEnemy(InventoryManager.instance.m_CurrentLightWeapon.m_WeaponDamage + 
                                                                           InventoryManager.instance.m_CurrentHeavyWeapon.m_WeaponDamage);

        return true;
    }
}
