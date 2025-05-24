using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool m_FakeHit;
    private int m_CurrentLane = 0; // -1 = left, 0 = middle, 1 = right

    public float m_DistanceBetweenLanes = 2f;

    private EndlessRunnerTileManager m_RunnerTileManager;
    private Animator m_Animator;

    public void Awake()
    {
        m_RunnerTileManager = FindFirstObjectByType<EndlessRunnerTileManager>();
        m_Animator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        m_Animator.SetBool("isFighting", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_FakeHit)
        {
            m_FakeHit = false;
            m_RunnerTileManager.ObstacleHit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(1);
        }   
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane(-1);
        }
    }

    private void ChangeLane(int direction)
    {
        if(m_CurrentLane == direction)
        {
            return;
        }

        m_CurrentLane += direction;

        transform.position = new Vector3(
            m_CurrentLane * m_DistanceBetweenLanes, 
            transform.position.y, 
            transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            other.enabled = false;
            m_RunnerTileManager.ObstacleHit();
            m_Animator.SetTrigger("isHitted");
        }
        if (other.CompareTag("Coin"))
        {
            // Coin collected, return it to pool and optionally play effect
            ObjectsPoolManager.m_Instance.ReturnCoin(other.gameObject);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("PremiumPickUp"))
        {
            // Premium Coin collected, return it to pool and optionally play effect
            ObjectsPoolManager.m_Instance.ReturnPremiumCoin(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
