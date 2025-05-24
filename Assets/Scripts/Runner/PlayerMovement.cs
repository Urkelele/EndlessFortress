using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource = null;

    [SerializeField] AudioClip m_CoinAudioclip = null;
    [SerializeField] AudioClip m_TomeAudioclip = null;
    [SerializeField] AudioClip m_ObstacleAudioclip = null;

    public List<Transform> m_LaneTransforms = new List<Transform>();
    private float m_SnapTreshold = 0.1f;
    public float m_SidewaysSpeed = 0f;
    private Vector2 m_StartTouchPos = Vector2.zero;

    //[SerializeField] AudioClip m_EndlessRunnerMusic = null;

    public bool m_FakeHit;
    [SerializeField] private int m_CurrentLane = 1; // 0 = left, 1 = middle, 2 = right

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
        PhoneInputs(Time.deltaTime);

        if(m_FakeHit)
        {
            m_FakeHit = false;
            m_RunnerTileManager.ObstacleHit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_CurrentLane++;
        }   
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_CurrentLane--;
        }

        MoveSideways(m_LaneTransforms[m_CurrentLane].position.x, Time.deltaTime);
    }

    public void MoveSideways(float newXPosition, float dt)
    {
        //If the player is close enough to the position we snap them in place, if not we change their zPosition accordingly
        if (Mathf.Abs(transform.position.x - newXPosition) < m_SnapTreshold)
        {
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
        else
        {
            if (transform.position.x < newXPosition)
            {
                transform.position += new Vector3(m_SidewaysSpeed * dt, 0, 0);
            }
            else
            {
                transform.position += new Vector3(-m_SidewaysSpeed * dt, 0, 0);
            }
        }

    }

    public void PhoneInputs(float dt)
    {
        if (Input.touchCount != 0)
        {
            Touch currentTouch = Input.GetTouch(0);

            if (currentTouch.phase == TouchPhase.Began)
            {
                m_StartTouchPos = currentTouch.position;
            }

            if (currentTouch.phase == TouchPhase.Ended)
            {
                Vector2 touchDirection = currentTouch.position - m_StartTouchPos;

                //If they dont intend to Jump, we check wether they want to go left or right
                if (touchDirection.x > 0 && m_CurrentLane != 2)
                {
                    m_CurrentLane++;
                }

                if (touchDirection.x < 0 /*&& m_CurrentLane != 0*/)
                {
                    m_CurrentLane--;
                }

            }
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
            // Hit Animation
            m_Animator.SetTrigger("isHitted");
            // Hit Audio
            PlayClip(m_ObstacleAudioclip);
        }
        if (other.CompareTag("Coin"))
        {
            // Coin collected, return it to pool and optionally play effect
            ObjectsPoolManager.m_Instance.ReturnCoin(other.gameObject);
            other.gameObject.SetActive(false);

            PlayClip(m_CoinAudioclip);
            

        }
        if (other.CompareTag("PremiumPickUp"))
        {
            // Premium Coin collected, return it to pool and optionally play effect
            ObjectsPoolManager.m_Instance.ReturnPremiumCoin(other.gameObject);
            other.gameObject.SetActive(false);

            PlayClip(m_TomeAudioclip);
        }
        
    }
    private void PlayClip(AudioClip audioClip)
    {
        m_AudioSource.clip = audioClip;
        m_AudioSource.Play();
    }

    
}


