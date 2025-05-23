using UnityEngine;

public class BigSwingFunctionality: BaseActiveScript
{
    public float m_DamageMultiplier = 2;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { UseActive(); }
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;

        FindAnyObjectByType<PlayerCombatScript>().DealDamageToAllEnemies(InventoryManager.instance.m_CurrentHeavyWeapon.m_WeaponDamage * m_DamageMultiplier);

        return true;
    }
}
