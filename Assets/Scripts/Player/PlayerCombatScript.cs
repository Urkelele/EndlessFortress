using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public HealthController m_PlayerHealthController = null;
    [SerializeField] InventoryManager m_Inventory = null;
    [SerializeField] EnemyBaseScript m_TargetEnemy = null;

    [SerializeField] float m_LightAttackDamage = 0;
    [SerializeField] float m_LightAttackTotalCooldown = 0;
    [SerializeField] float m_LightAttackCurrentCooldown = 0;

    [SerializeField] float m_HeavyAttackDamage = 0;
    [SerializeField] float m_HeavyAttackTotalCooldown = 0;
    [SerializeField] float m_HeavyAttackCurrentCooldown = 0;
    private void OnEnable()
    {
        m_LightAttackDamage = m_Inventory.m_CurrentLightWeapon.ItemScript.m_WeaponDamage;
        m_
        m_LightAttackCurrentCooldown = m_LightAttackTotalCooldown;
        m_HeavyAttackCurrentCooldown = m_HeavyAttackTotalCooldown;
        //Call ItemManagers Get LightAttack
        //Call ItemManagers Get HeavyAttack
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(float dmg)
    {
        m_PlayerHealthController.ReceiveDamage(dmg);
        //Receive Damage Animation
    }

    public void UseAbility()
    {
        //AbiltyManager function for using the ability
        //Abilitys visula effect
    }

    public void LightAttack()
    {
        if (m_LightAttackCurrentCooldown < 0.0f)
        {
            //Reset timer
            m_LightAttackCurrentCooldown = m_LightAttackTotalCooldown;
            m_TargetEnemy.m_HealthController.ReceiveDamage(m_LightAttackDamage);
        }
        //Attack Animation
    }

    public void HeavyAttack()
    {
        if (m_HeavyAttackCurrentCooldown < 0.0f)
        {
            //Reset timer
            m_HeavyAttackCurrentCooldown = m_HeavyAttackTotalCooldown;
            m_TargetEnemy.m_HealthController.ReceiveDamage(m_HeavyAttackDamage);
        }
        //Attack Animation
    }

    public void UseActiveItem()
    {
        //Call ItemManagers.ActiveItem.Action()
        //Attack Animation

    }
}
