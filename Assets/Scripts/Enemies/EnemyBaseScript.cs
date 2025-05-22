using UnityEditor;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    [Header("BASE ENEMY CLASSES")]
    //Scripts
    public HealthController m_HealthController = null;
    private PlayerCombatScript m_PlayerCombatScript = null;
    public Collider m_Collider = null;
    private Animator m_Animator = null;
    private ClickDetection m_ClickDetection = null;
    private Outline m_Outline = null;

    [Header("COOLDOWNS")]
    [SerializeField] private float m_TotalActionCooldown = 0.0f;
    [SerializeField] public float m_CurrentActionCooldown = 0.0f;
    
    private void Start()
    {
        //Get references
        m_HealthController = GetComponent<HealthController>();
        m_Collider = GetComponent<Collider>();
        m_Animator = GetComponent<Animator>();
        m_ClickDetection = GetComponent<ClickDetection>();
        m_Animator = GetComponent<Animator>();
        m_Outline = GetComponent<Outline>();
        m_PlayerCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatScript>();

        m_CurrentActionCooldown = m_TotalActionCooldown;
    }

    private void Update()
    {
        m_CurrentActionCooldown -= Time.deltaTime;

        if (m_CurrentActionCooldown < 0.0f)
        {
            //Reset timer
            m_CurrentActionCooldown = m_TotalActionCooldown;
            PerformAction();
        }

    
        // If an enemy is the last object clicked that means that it also is the current target enemy
        if(m_ClickDetection.m_IsLastObjectClicked)
        {
            OnClick();
        }
        else
        {
            m_Outline.enabled = false;
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
    }

}
