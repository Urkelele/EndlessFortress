using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public float m_MaxHealth;
    public float m_CurrentHealth;

    [SerializeField] private Image m_HealthBar;

    private PlayerHealthController m_PlayerHealthController;

    private void Start()
    {
        m_PlayerHealthController = FindAnyObjectByType<PlayerHealthController>();
    }
    public void SetMaxHealth()
    {
        m_MaxHealth = m_PlayerHealthController.m_MaxHealthPoints;
    }
    public void SetHealth()
    {
        m_CurrentHealth = m_PlayerHealthController.m_CurrentHealthPoints;
        m_HealthBar.fillAmount = (m_PlayerHealthController.m_CurrentHealthPoints / m_MaxHealth);
    }
    private void Update()
    {
        SetMaxHealth();
        SetHealth();
    }
}
