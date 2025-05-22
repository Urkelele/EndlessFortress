using UnityEngine;

public class BigSwingFunctionality: BaseActiveScript
{
    CombatManager m_CombatManager;
    InventoryManager m_InventoryManager;

    public float m_DamageMultiplyier = 2;

    public void Start()
    {
        m_CombatManager = FindAnyObjectByType<CombatManager>();
        m_InventoryManager = FindAnyObjectByType<InventoryManager>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { UseActive(); }
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;
        
        foreach (EnemyBaseScript enemyScript in m_CombatManager.m_CombatEnemies)
        {
            enemyScript.gameObject.GetComponent<HealthController>().ReceiveDamage(m_InventoryManager.m_CurrentHeavyWeapon.m_WeaponDamage * m_DamageMultiplyier);
        }
        return true;
    }
}
