using UnityEditor;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public float m_MaxHealth;
    public float m_CurrentHealth;
    private float m_Width;
    private float m_Height;

    [SerializeField] private RectTransform m_HealthBar;

    private PlayerHealthController m_PlayerHealthController;

    private void Start()
    {
        m_Width = GetComponent<RectTransform>().sizeDelta.x;
        m_Height = GetComponent<RectTransform>().sizeDelta.y;
        m_PlayerHealthController = FindAnyObjectByType<PlayerHealthController>();
    }

    public void SetMaxHealth()
    {
        m_MaxHealth = m_PlayerHealthController.m_MaxHealthPoints;
    }
    public void SetHealth()
    {
        m_CurrentHealth = m_PlayerHealthController.m_CurrentHealthPoints;
        float newWidth = (m_PlayerHealthController.m_CurrentHealthPoints / m_MaxHealth) * m_Width;
        m_HealthBar.sizeDelta = new Vector2(newWidth, m_Height);
    }
    private void Update()
    {
        SetMaxHealth();
        SetHealth();
    }
}
