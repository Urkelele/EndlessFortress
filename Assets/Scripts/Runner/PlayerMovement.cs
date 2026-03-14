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

    public GameObject m_RunnerSword = null;
    public List<Transform> m_LaneTransforms = new List<Transform>();
    private float m_SnapTreshold = 0.1f;
    public float m_SidewaysSpeed = 0f;
    private Vector2 m_StartTouchPos = Vector2.zero;

    [Header("JUMP PARAMS")]
    public float m_JumpSpeed = 5f;
    private bool m_IsJumping = false;
    private int m_JumpDirection = 1;
    private bool m_IsGoingUp = false;
    public float m_JumpHeight = 2f;

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

    private void OnEnable()
    {
        m_RunnerSword.SetActive(true);
    }

    private void OnDisable()
    {
        m_Animator.SetBool("isFighting", true);   
        m_RunnerSword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Stop the time if the TimeManager says so
        if(TimeManager.instance.m_StopTime) { return; }
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
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Debug.LogError("Jump");
            m_JumpDirection = 1;
            m_IsJumping = true;
            m_IsGoingUp = true;

            m_Animator.SetTrigger("isJumping");
        }


        if(m_IsJumping)
        {
            Jump(Time.deltaTime);
        }

        //Check for currentLane out of bounds
        if (m_CurrentLane < 0) m_CurrentLane = 0;
        if (m_CurrentLane > 2) m_CurrentLane = 2;
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
                
                //If the vertical distance traveled by the finger is enough, we assume they want to jump
                if (touchDirection.y > 200 && !m_IsJumping)
                {
                    m_JumpDirection = 1;
                    m_IsJumping = true;
                    m_IsGoingUp = true;

                    m_Animator.SetTrigger("isJumping");
                }
                else
                {
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
    }

    private void Jump(float dt)
    {
        //We constatly modify the value of the players position depending on their jumpDirection
        transform.position += new Vector3(0, m_JumpSpeed * dt * m_JumpDirection, 0);

        //When the player reaches the jumpPosition we change the direction of the jump
        if (transform.position.y > m_JumpHeight && m_IsGoingUp)
        {
            m_JumpDirection = -1;
            m_IsGoingUp = false;
        }

        //Once we are close enough to the ground we snap the player in place
        if (transform.position.y < m_SnapTreshold && !m_IsGoingUp)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            m_IsJumping = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            other.enabled = false;
            m_RunnerTileManager.ObstacleHit();
            // Hit Audio
            PlayClip(m_ObstacleAudioclip);
        }
        if (other.CompareTag("Coin"))
        {
            // Coin collected, return it to pool and optionally play effect
            ObjectsPoolManager.m_Instance.ReturnCoin(other.gameObject);
            InventoryManager.instance.AddGold(1);
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


