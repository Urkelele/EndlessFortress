using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : HealthController
{
    public float m_BaseHp = 100f;
    public Animator m_Animator = null;


    protected override void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_MaxHealthPoints = m_BaseHp;
        m_CurrentHealthPoints = m_MaxHealthPoints;
    }

    protected override void Update()
    {
        base.Update();

        if(m_IsDead && TimeManager.instance.m_StopTime == false)
        {
            InventoryManager.instance.EnableItemTrigger(TriggerType.PLAYER_DEATH);
            GeneralCanvasManager.instance.RunFinished();
        }

        //DEBUG
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Revive(m_MaxHealthPoints);
        }
        
        m_Animator.SetBool("isDead", m_IsDead);

    }

    public override void ReceiveDamage(float damageReceived)
    {
        base.ReceiveDamage(damageReceived);

        //Receive Damage Animation
        m_Animator.SetTrigger("isHitted");
    }

    public void RestartLife()
    {
        m_CurrentHealthPoints = m_MaxHealthPoints;
        m_IsDead = false;

        Debug.LogError("IS TIME STOPPED?: " + TimeManager.instance.m_StopTime);
        Debug.LogError("IS DEAD?: " + m_IsDead);
        Debug.LogWarning("[REVIVE]");

        GeneralCanvasManager.instance.Revive();

    }

    public void Revive(float revivedHealth)
    {
        //Change player health
        m_CurrentHealthPoints = revivedHealth;
        //Start time
        TimeManager.instance.m_StopTime = false;
    }
}
