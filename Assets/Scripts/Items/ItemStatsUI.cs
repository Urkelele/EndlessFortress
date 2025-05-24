using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class ItemStatsUI : MonoBehaviour
{
    public static ItemStatsUI ShowItemUI_Instance;

    public GameObject m_Panel; // The UI panel
    public TextMeshProUGUI m_NameItem;
    public TextMeshProUGUI m_DescriptionItem;
    public TextMeshProUGUI m_StatsTextItem;
    public Image m_IconImageItem;

    private void Awake()
    {
        ShowItemUI_Instance = this;
        m_Panel.SetActive(false);
    }

    public void ShowItemInfo(ItemBaseScript itemBaseScript, bool isShown)
    {
        if(!isShown)
        {
            Hide();
            return;
        }
        m_NameItem.text = itemBaseScript.m_ItemName;
        m_DescriptionItem.text = itemBaseScript.m_Description;
        m_StatsTextItem.text = $"Attack +{itemBaseScript.m_AttackSpeedMultiplier}\nHealth +{itemBaseScript.m_ExtraHealth}";
        m_IconImageItem.sprite = itemBaseScript.m_SpriteItem;

        m_Panel.SetActive(true);
    }

    public void Hide()
    {
        m_Panel.SetActive(false);
    }    
}
