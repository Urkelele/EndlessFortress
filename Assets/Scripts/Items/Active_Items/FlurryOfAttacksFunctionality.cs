using System.Collections;
using UnityEngine;

public class FlurryOfAttacksFunctionality : BaseActiveScript
{
    public int m_NumberOfAttack = 5;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) { UseActive(); }
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;

        StartCoroutine(DoDamage(InventoryManager.instance.m_CurrentLightWeapon.m_WeaponDamage));
        return true;
    }
    private IEnumerator DoDamage(float damage)
    {
        PlayerCombatScript playerCombatScript = FindAnyObjectByType<PlayerCombatScript>();
        for (int i = 0; i < m_NumberOfAttack; i++)
        {
            playerCombatScript.DealDamageToTargetEnemy(damage);
            Debug.Log("Do " + damage + " damage");
            yield return new WaitForSeconds(0.1f);
        }
    }
}
