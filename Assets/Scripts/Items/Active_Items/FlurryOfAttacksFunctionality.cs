using System.Collections;
using UnityEngine;

public class FlurryOfAttacksFunctionality : BaseActiveScript
{
    InventoryManager m_InventoryManager;

    public int m_NumberTimesAttack = 5;
    public void Start()
    {
        m_InventoryManager = FindAnyObjectByType<InventoryManager>();
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) { UseActive(); }
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;

        StartCoroutine(DoDamage(m_InventoryManager.m_CurrentLightWeapon.m_WeaponDamage));
        return true;
    }
    private IEnumerator DoDamage(float damage)
    {        
        for (int i = 0; i < m_NumberTimesAttack; i++)
        {
            //m_CombatManager.m_CurrentEnemyTarget.m_HealthController.ReceiveDamage(damage);
            Debug.Log("Do " + damage + " damage");
            yield return new WaitForSeconds(0.1f);
        }
    }
}
