using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public EnemyBaseScript m_TargetEnemy = null;
    public HealthController m_PlayerHealthController = null;
    private CombatManager m_CombatManager = null;
    [SerializeField] AbilityManager m_AbilityManager = null;
    [SerializeField] InventoryManager m_Inventory = null;
    
    [Header("Light Attack Variables")]
    [SerializeField] private float m_LightAttackDamage = 0;
    [SerializeField] private float m_LightAttackTotalCooldown = 0;
    [SerializeField] private float m_LightAttackCurrentCooldown = 0;
    [Header("Heavy Attack Variables")]
    [SerializeField] private float m_HeavyAttackDamage = 0;
    [SerializeField] private float m_HeavyAttackTotalCooldown = 0;
    [SerializeField] private float m_HeavyAttackCurrentCooldown = 0;

    private void Start()
    {
        m_PlayerHealthController = GetComponent<HealthController>();
        m_CombatManager = FindAnyObjectByType<CombatManager>();
        m_Inventory = FindAnyObjectByType<InventoryManager>();
        m_AbilityManager = FindAnyObjectByType<AbilityManager>();
    }
    private void OnEnable()
    {
        //When the script is enabled (the battle starts) update the values from the weapons of the inventory to use in the fight
        Debug.Log(m_Inventory.m_CurrentLightWeapon.m_WeaponDamage);
        m_LightAttackDamage = m_Inventory.m_CurrentLightWeapon.m_WeaponDamage;
        m_LightAttackTotalCooldown = m_Inventory.m_CurrentLightWeapon.m_AttackCooldown;
        m_LightAttackCurrentCooldown = m_LightAttackTotalCooldown;

        m_HeavyAttackDamage = m_Inventory.m_CurrentHeavyWeapon.m_WeaponDamage;
        m_HeavyAttackTotalCooldown = m_Inventory.m_CurrentHeavyWeapon.m_AttackCooldown;
        m_HeavyAttackCurrentCooldown = m_HeavyAttackTotalCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        m_LightAttackCurrentCooldown -= Time.deltaTime;
        m_HeavyAttackCurrentCooldown -= Time.deltaTime;

    }

    public void GetHit(float dmg)
    {
        m_PlayerHealthController.ReceiveDamage(dmg);
        //Receive Damage Animation
    }

    public void UseAbility()
    {
        m_AbilityManager.UseAbility();
        //Abilitys visula effect
    }

    public void LightAttack()
    {
        if (m_LightAttackCurrentCooldown < 0.0f)
        {
            //Reset timer
            m_LightAttackCurrentCooldown = m_LightAttackTotalCooldown;
            m_CombatManager.m_CurrentEnemyTarget.m_HealthController.ReceiveDamage(m_LightAttackDamage);
        }
        //Attack Animation
    }

    public void HeavyAttack()
    {
        if (m_HeavyAttackCurrentCooldown < 0.0f)
        {
            //Reset timer
            m_HeavyAttackCurrentCooldown = m_HeavyAttackTotalCooldown;
            m_CombatManager.m_CurrentEnemyTarget.m_HealthController.ReceiveDamage(m_HeavyAttackDamage);
        }
        //Attack Animation
    }

    public void UseActiveItem()
    {
        //Call ItemManagers.ActiveItem.Action()
        //Attack Animation

    }
}
