using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Items/Active_Items/FlurryOfAttacks")]
public class FlurryOfAttacksFunctionality : BaseActiveScript
{
    public int m_NumberOfAttack = 5;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) { UseActive(); }
    }
    public override void UseActive()
    {
        base.UseActive();
        //StartCoroutine(DoDamage(InventoryManager.instance.m_CurrentLightWeapon.m_ItemDamage));        
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
