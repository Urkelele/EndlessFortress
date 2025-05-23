using UnityEditor;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    [Header("BASE ENEMY CLASSES")]
    //Scripts
    public HealthController m_HealthController = null;
    protected PlayerCombatScript m_PlayerCombatScript = null;
    public Collider m_Collider = null;
    private Animator m_Animator = null;
    public ClickDetection m_ClickDetection = null;
    private Outline m_Outline = null;
    public bool m_IsCurrentTarget = false;
    public bool m_IsBoss = false;

    [Header("Enemy Stats")]
    [SerializeField] protected float m_TotalActionCooldown = 0.0f;
    [SerializeField] public float m_CurrentActionCooldown = 0.0f;
    
    [SerializeField] protected int m_GoldReward = 0;

    //Control vars
    private bool m_OnDeathTriggered = false; //Control bool so that the OnDeath method is only called once
    protected virtual void Start()
    {
        //Get references
        m_HealthController = GetComponent<HealthController>();
        m_Collider = GetComponent<Collider>();
        m_ClickDetection = GetComponent<ClickDetection>();
        m_Animator = GetComponent<Animator>();
        m_Outline = GetComponent<Outline>();
        m_PlayerCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatScript>();

        m_CurrentActionCooldown = m_TotalActionCooldown;
    }

    protected virtual void Update()
    {
        m_CurrentActionCooldown -= Time.deltaTime;

        if (m_CurrentActionCooldown < 0.0f)
        {
            //Reset timer
            m_CurrentActionCooldown = m_TotalActionCooldown;
            PerformAction();
        }

        // If an enemy is the last object clicked that means that it also is the current target enemy
        if (m_ClickDetection.m_IsLastObjectClicked)
        {
            OnClick();
        }
        else
        {
            m_Outline.enabled = false;
            m_IsCurrentTarget = false;
        }

        if(m_HealthController.m_IsDead && !m_OnDeathTriggered)
        {
            OnDeath();
        }
    }

    public virtual void PerformAction()
    {

    }

    private void OnClick()
    {
        // When enemy is clicked they are targeted by the player, m_TargetEnemy is type EnemyBaseScript
        m_PlayerCombatScript.m_TargetEnemy = this;
        m_Outline.enabled = true;
        m_IsCurrentTarget = true;
    }

    public virtual void OnDeath()
    {
        m_OnDeathTriggered = true;

        PlayerStats.instance.m_EnemiesSlain += 1;

        //Add the enemy's gold reward to the total pool of gold that will be given to the player when the battle finishes
        CombatManager.instance.m_GoldBattleReward += m_GoldReward;

        //Tell the inventory that an enemy died so that it can call triggeredItems
        InventoryManager.instance.EnableItemTrigger(TriggerType.ENEMY_DEATH);

        //Dead enemies out of the combat list
        CombatManager.instance.m_CombatEnemies.Remove(this);
        //DEATH ANIMATION
    }

}
