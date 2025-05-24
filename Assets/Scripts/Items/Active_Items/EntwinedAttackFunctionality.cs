using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Items/Active_Items/EntwinedAttack")]
public class EntwinedAttackFunctionality : BaseActiveScript
{
    public override void UseActive()
    {
        base.UseActive();
        //Both weapons deal damage equal to their attack to an enemy
        FindAnyObjectByType<PlayerCombatScript>().DealDamageToTargetEnemy(InventoryManager.instance.m_CurrentLightWeapon.m_ItemDamage + 
                                                                           InventoryManager.instance.m_CurrentHeavyWeapon.m_ItemDamage);
    }
}
