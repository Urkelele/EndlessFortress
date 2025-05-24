using UnityEngine;

public class ShopRerollController : MonoBehaviour
{
    public bool m_HasRerrolled = false;
    public GameObject m_RerollPanel;
    [SerializeField] private ShopController m_ShopController;

    public void SpawnReroll(ShopController shopController, bool hasRerrolled)
    {
        m_ShopController = shopController;
        m_HasRerrolled = hasRerrolled;
        if(!m_HasRerrolled)
        {
            m_RerollPanel.SetActive(true);
            GetComponent<Animator>().SetTrigger("Play");
        }
        else
        {
            m_RerollPanel.SetActive(false);
        }
    }

    public void RerollActivated()
    {
        Debug.Log("Player shaw video to reroll");
        m_HasRerrolled = true;
        GetComponent<Animator>().SetTrigger("Backwards");
        m_ShopController.RerollItemsInShop();
    }

    public void FinishedBackwardsAnimation()
    {
        Debug.Log("closed");
        m_RerollPanel.SetActive(false );
    }

}
