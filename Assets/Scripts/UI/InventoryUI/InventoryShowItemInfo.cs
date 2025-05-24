using TMPro;
using UnityEngine;

public class InventoryShowItemInfo : MonoBehaviour
{
    public InventoryControllerUI m_InventoryControllerUI;
    public GameObject m_ItemInfoPanel;

    public TextMeshProUGUI m_NameText;
    public TextMeshProUGUI m_DescriptionText;
    public TextMeshProUGUI m_TypeItemText;


    public void ShowItemInfo(ItemBaseScript item)
    {
        m_ItemInfoPanel.SetActive(true);
        m_NameText.text = item.m_ItemName;
        m_DescriptionText.text = item.m_Description;

        if(m_TypeItemText != null)
        {
            switch (item.m_TypeItem)
            {
                case ItemBaseScript.ItemType.PASSIVE:
                    m_TypeItemText.text = "Passive";
                    break;
                case ItemBaseScript.ItemType.ACTIVE:
                    m_TypeItemText.text = "Active";
                    break;
                case ItemBaseScript.ItemType.LIGHT_WEAPON:
                    m_TypeItemText.text = "Light Weapon";
                    break;
                case ItemBaseScript.ItemType.HEAVY_WEAPON:
                    m_TypeItemText.text = "Heavy Weapon";
                    break;
            }
        }
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
