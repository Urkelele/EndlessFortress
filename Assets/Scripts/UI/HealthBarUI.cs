using UnityEditor;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public float m_MaxHealth;
    public float m_CurrentHealth;
    private float m_Width;
    private float m_Height;

    [SerializeField] private RectTransform m_HealthBar;

    private void Start()
    {
        m_Width = GetComponent<RectTransform>().sizeDelta.x;
        m_Height = GetComponent<RectTransform>().sizeDelta.y;
    }

    public void SetMaxHealth(float maxHealth)
    {
        m_MaxHealth = maxHealth;
    }
    public void SetHealth(float health)
    {
        m_CurrentHealth = health;
        float newWidth = (health / m_MaxHealth) * m_Width;
        m_HealthBar.sizeDelta = new Vector2(newWidth, m_Height);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SetHealth(m_CurrentHealth);
        }
    }
}
