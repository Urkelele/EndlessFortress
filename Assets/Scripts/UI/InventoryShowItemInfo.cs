using TMPro;
using UnityEngine;

public class InventoryShowItemInfo : MonoBehaviour
{
    public InventoryControllerUI m_InventoryControllerUI;
    public GameObject m_ItemInfoPanel;

    public TextMeshProUGUI m_NameText;
    public TextMeshProUGUI m_DescriptionText;

    public void ShowItemInfo(ItemBaseScript item)
    {
        m_ItemInfoPanel.SetActive(true);
        m_NameText.text = item.m_ItemName;
        m_DescriptionText.text = item.m_Description;
        GetComponent<Animator>().SetTrigger("Play");
    }

    public void HideItemInfo()
    {
        GetComponent<Animator>().SetTrigger("Backwards");
    }

    public void FinishedBackwardsAnimation()
    {
        m_ItemInfoPanel.SetActive(false);
    }
}
