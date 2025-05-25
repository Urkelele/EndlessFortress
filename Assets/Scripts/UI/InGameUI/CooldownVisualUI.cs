using UnityEngine;
using UnityEngine.UI;

public class CooldownVisualUI : MonoBehaviour
{
    public Image m_Image;
    [SerializeField] private float m_Cooldown;
    [SerializeField] private bool m_IsCooldown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Stop the time if the TimeManager says so
        if (TimeManager.instance.m_StopTime) { return; }

        if (m_IsCooldown)
        {
            m_Image.fillAmount -= 1 / m_Cooldown * Time.deltaTime;

            if (m_Image.fillAmount <= 0)
            {
                m_Image.fillAmount = 0;
                m_IsCooldown = false;
            }
        }
    }

    public bool TryButton(float attackCooldown)
    {
        Debug.Log(attackCooldown);
        if (m_IsCooldown)
        {
            return false;
        }
        else
        {
            m_IsCooldown = true;
            m_Image.fillAmount = 1;
            m_Cooldown = attackCooldown;
            return true;
        }
    }

    public bool IsInCooldown()
    {
        return m_IsCooldown;
    }
}
