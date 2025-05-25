using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadMenuController : MonoBehaviour
{
    [SerializeField] private ExternalDataManager m_ExternalDataManager;
    public GameObject m_RespawPanel;
    public TextMeshProUGUI m_TextAmountTomes;
    public Button m_PayButtonWithTomes;
    public Button m_SeeVideoButton;
    [SerializeField] private int m_AmountOfTomes;
    public bool m_HasSpawnedUsingVideo = false;

    [Header("AUDIO")]
    [SerializeField] AudioSource m_AudioSource = null;

    [SerializeField] AudioClip m_Dying = null;
    

    private void OnEnable()
    {
        if (m_ExternalDataManager == null) { m_ExternalDataManager = FindAnyObjectByType<ExternalDataManager>(); }

        m_RespawPanel.SetActive(true);

        // Take the text and take only the numbers without the Pay: 
        string aux = m_TextAmountTomes.text.Substring(5);
        m_AmountOfTomes = int.Parse(aux);
        if (m_ExternalDataManager.m_StoredData.m_AmountTomes > m_AmountOfTomes)
        {
            m_PayButtonWithTomes.interactable = true;
        }
        else
        {
            m_PayButtonWithTomes.interactable = false;
        }
        if(m_HasSpawnedUsingVideo)
        {
            m_SeeVideoButton.gameObject.SetActive(false);
        }
        else
        {
            m_SeeVideoButton.gameObject.SetActive(true);
        }

        PlayClip(m_Dying);
    }
    public void RespawnWithTomes()
    {
        if(m_ExternalDataManager.m_StoredData.m_AmountTomes > m_AmountOfTomes)
        {
            m_ExternalDataManager.AddTomes(-m_AmountOfTomes);
            GameObject.FindAnyObjectByType<PlayerHealthController>().RestartLife();
            Debug.Log("Player respawned");
            //gameObject.SetActive(true);
        }
    }

    public void RespawnWithVideo()
    {
        Debug.Log("Player Respawned Using Video");
        m_HasSpawnedUsingVideo = true;
        gameObject.SetActive(true );
       
    }
    public void DenyRespawn()
    {
        m_RespawPanel.SetActive(false);
    }

    private void PlayClip(AudioClip audioClip)
    {
        m_AudioSource.clip = audioClip;
        m_AudioSource.Play();
    }
}
