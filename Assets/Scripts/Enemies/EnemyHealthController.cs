using UnityEngine;

public class EnemyHealthController : HealthController
{
    private Animator m_Animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        base.Awake();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void ReceiveDamage(float damageReceived)
    {
        base.ReceiveDamage(damageReceived);

        //Receive Damage Animation
        m_Animator.SetTrigger("isHitted");
    }

}
