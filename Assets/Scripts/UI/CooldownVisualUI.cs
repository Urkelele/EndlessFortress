using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CooldownVisualUI : MonoBehaviour
{
    public Image m_Image;
    public float m_Cooldown;
    bool m_IsCooldown = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && !m_IsCooldown)
        {
            m_IsCooldown = true;
            m_Image.fillAmount = 1;
        }
        if(m_IsCooldown)
        {
            m_Image.fillAmount -= 1 / m_Cooldown * Time.deltaTime;

            if(m_Image.fillAmount <= 0)
            {
                m_Image.fillAmount = 0;
                m_IsCooldown= false;
            }
        }
    }
}
