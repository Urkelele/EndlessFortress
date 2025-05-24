using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public PlayerHealthController m_PlayerHealthController = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_PlayerHealthController = FindAnyObjectByType<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            m_PlayerHealthController.ReceiveDamage(m_PlayerHealthController.m_MaxHealthPoints/4);
        }
    }
}
