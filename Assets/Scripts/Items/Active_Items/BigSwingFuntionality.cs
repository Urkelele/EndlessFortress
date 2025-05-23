using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Items/Active_Items/BigSwing")]
public class BigSwingFunctionality: BaseActiveScript
{
    public float m_DamageMultiplier = 2;

    public override void UseActive()
    {
        base.UseActive();
        //All enemies receive damage = to 2x heavyweapons damage
        FindAnyObjectByType<PlayerCombatScript>().DealDamageToAllEnemies(InventoryManager.instance.m_CurrentHeavyWeapon.m_ItemDamage * m_DamageMultiplier);
    }
}
