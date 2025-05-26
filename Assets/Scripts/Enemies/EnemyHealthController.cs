using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyHealthController : HealthController
{
    private Animator m_Animator;
    public UnityEngine.UI.Slider m_HealthBarSlider = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        base.Awake();
        m_Animator = GetComponent<Animator>();
        m_HealthBarSlider = GetComponentInChildren<UnityEngine.UI.Slider>();
        m_HealthBarSlider.maxValue = m_MaxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        m_HealthBarSlider.value = m_CurrentHealthPoints;
        if(m_HealthBarSlider.value == 0)
        {
            m_HealthBarSlider.transform.GetChild(1).gameObject.SetActive(false); //Second child is slider fill, deactivate it when the enemy dies so that the red health bar disappears
        }
    }

    public override void ReceiveDamage(float damageReceived)
    {
        base.ReceiveDamage(damageReceived);

        //Receive Damage Animation
        m_Animator.SetTrigger("isHit");
    }

}
