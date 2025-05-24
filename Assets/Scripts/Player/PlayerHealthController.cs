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

        if(m_IsDead)
        {
            // Dead Animation
            m_Animator.SetTrigger("isDead");

            InventoryManager.instance.EnableItemTrigger(TriggerType.PLAYER_DEATH);
        }
    }

    public override void ReceiveDamage(float damageReceived)
    {
        base.ReceiveDamage(damageReceived);

        //Receive Damage Animation
        m_Animator.SetTrigger("isHitted");
    }
}
