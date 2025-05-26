using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    [SerializeField] public EnemyBaseScript m_TargetEnemy = null;

    [Header("References")]
    public HealthController m_PlayerHealthController = null;
    public Animator m_Animator = null;
    public GameObject m_CombatSword = null;

    [Header("Light Attack Variables")]
    [SerializeField] private float m_LightAttackDamage = 0;
    [SerializeField] private float m_LightAttackTotalCooldown = 0;
    [SerializeField] private float m_LightAttackCurrentCooldown = 0;
    [Header("Heavy Attack Variables")]
    [SerializeField] private float m_HeavyAttackDamage = 0;
    [SerializeField] private float m_HeavyAttackTotalCooldown = 0;
    [SerializeField] private float m_HeavyAttackCurrentCooldown = 0;

    [Header("AUDIO")]
    [SerializeField] AudioSource m_AudioSource = null;
    [SerializeField] AudioClip m_AttackSound = null;



    private void Awake()
    {
        m_PlayerHealthController = GetComponent<HealthController>();
        m_Animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        //When the script is enabled (the battle starts) update the values from the weapons of the inventory to use in the fight
        m_LightAttackDamage = InventoryManager.instance.m_CurrentLightWeapon.m_ItemDamage;
        m_LightAttackTotalCooldown = InventoryManager.instance.m_CurrentLightWeapon.m_ItemCooldown;
        m_LightAttackCurrentCooldown = m_LightAttackTotalCooldown;

        m_HeavyAttackDamage = InventoryManager.instance.m_CurrentHeavyWeapon.m_ItemDamage;
        m_HeavyAttackTotalCooldown = InventoryManager.instance.m_CurrentHeavyWeapon.m_ItemCooldown;
        m_HeavyAttackCurrentCooldown = m_HeavyAttackTotalCooldown;

        m_Animator.SetBool("isFighting", true);
        m_CombatSword.SetActive(true);
    }

    private void OnDisable()
    {
        m_Animator.SetBool("isFighting", false);
        m_CombatSword.SetActive(false);
    }

    private void Start()
    {
        //The player will first be running, so disable to combat script
        GetComponent<PlayerCombatScript>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_LightAttackCurrentCooldown -= Time.deltaTime;
        m_HeavyAttackCurrentCooldown -= Time.deltaTime;

        //Make target enemy have outline
        m_TargetEnemy.GetComponent<Outline>().enabled = true;
    }

    public void GetHit(float dmg)
    {
        m_PlayerHealthController.ReceiveDamage(dmg);
        
    }

    public void UseAbility()
    {
        AbilityManager.instance.UseAbility();
    }

    public void LightAttack()
    {
        if (m_TargetEnemy != null)
        {
            //Reset timer, take into account attack speed reduction
            DealDamageToTargetEnemy(m_LightAttackDamage);
            
            //Attack Animation
            m_Animator.SetTrigger("isAttacking");

            m_AudioSource.clip = m_AttackSound;
            m_AudioSource.Play();
        }
    }

    public void HeavyAttack()
    {
        if (m_TargetEnemy != null)
        {
            //Reset timer, take into account attack speed reduction
            m_HeavyAttackCurrentCooldown = m_HeavyAttackTotalCooldown * (1/InventoryManager.instance.m_TotalAttackSpeedMultiplier);
            DealDamageToTargetEnemy(m_HeavyAttackDamage);

            //Attack Animation
            m_Animator.SetTrigger("isAttacking");

            m_AudioSource.clip = m_AttackSound;
            m_AudioSource.Play();
        }
    }

    /// <summary>
    /// Deals damage to current target enemy, takes into account lifesteal and attack mult
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamageToTargetEnemy(float damage)
    {
        float totalDamage = damage * InventoryManager.instance.m_TotalAttackDamageMultiplier;
        m_TargetEnemy.m_HealthController.ReceiveDamage(totalDamage);
        m_PlayerHealthController.HealDamage(totalDamage * InventoryManager.instance.m_TotalLifeSteal);
    }

    /// <summary>
    /// Deals damage to all enemies, takes into account lifesteal and attack mult
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamageToAllEnemies(float damage)
    {
        float totalDamage = damage * InventoryManager.instance.m_TotalAttackDamageMultiplier;
     
        foreach (EnemyBaseScript enemy in CombatManager.instance.m_CombatEnemies)
        {
            enemy.m_HealthController.ReceiveDamage(totalDamage);
            m_PlayerHealthController.HealDamage(totalDamage * InventoryManager.instance.m_TotalLifeSteal);
        }

    }
    public void UseActiveItem()
    {
        if(m_TargetEnemy != null)
        {
            //Call ItemManagers.ActiveItem.Action()

            InventoryManager.instance.m_CurrentActiveItem.UseActive();

            //Attack Animation
            m_Animator.SetTrigger("isAttacking");

            m_AudioSource.clip = m_AttackSound;
            m_AudioSource.Play();
        }
    }
}
