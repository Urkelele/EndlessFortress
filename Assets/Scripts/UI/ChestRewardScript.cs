using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChestRewardScript : MonoBehaviour
{
    public GameObject m_ChestRewardPanel;

    [Header("ItemRewardAttributes")]
    public GameObject m_ItemRewardPanel;
    public Image m_ItemIcon;
    public TextMeshProUGUI m_ItemNameText;
    public TextMeshProUGUI m_ItemDescriptionText;
    public Image m_ItemIconBox;
    public Sprite[] m_ItemBoxSprites;
    public TextMeshProUGUI m_ItemTypeText;
    [SerializeField] private ItemBaseScript m_ItemReward;

    public void SpawnChestReward()
    {
        CombatManager.instance.GiveRewards(true, true, false);
        ActivateItemReward(CombatManager.instance.m_ItemReward);
        m_ChestRewardPanel.SetActive(true);
    }

    public void ActivateItemReward(ItemBaseScript itemBaseScript)
    {
        m_ItemReward = itemBaseScript;
        m_ItemRewardPanel.SetActive(true);
        if(itemBaseScript == null) { return; }
        m_ItemIconBox.sprite = m_ItemBoxSprites[(int)itemBaseScript.m_QualityItem];
        m_ItemIcon.sprite = itemBaseScript.m_SpriteItem;
        m_ItemNameText.text = itemBaseScript.m_ItemName;
        m_ItemDescriptionText.text = itemBaseScript.m_Description;
        switch (itemBaseScript.m_TypeItem)
        {
            case ItemBaseScript.ItemType.PASSIVE:
                m_ItemTypeText.text = "Passive";
                break;
            case ItemBaseScript.ItemType.ACTIVE:
                m_ItemTypeText.text = "Active";
                break;
            case ItemBaseScript.ItemType.LIGHT_WEAPON:
                m_ItemTypeText.text = "Light weapon";
                break;
            case ItemBaseScript.ItemType.HEAVY_WEAPON:
                m_ItemTypeText.text = "Heavy weapon";
                break;
        }
    }

    public void CloseChestReward()
    {
        m_ItemRewardPanel.SetActive(false);
        m_ChestRewardPanel.SetActive(false);
        RoomTransitionManager.instance.RoomTransition(TransitionType.RUNNER);
    }
    public void TakeRewardedItem()
    {
        ItemBaseScript itemReward = CombatManager.instance.m_ItemReward;
        switch (itemReward.m_TypeItem)
        {
            case ItemBaseScript.ItemType.PASSIVE:
                InventoryManager.instance.AddNewPassiveItem(itemReward);
                break;
            case ItemBaseScript.ItemType.ACTIVE:
                InventoryManager.instance.AddNewActive((BaseActiveScript)itemReward);
                break;
            case ItemBaseScript.ItemType.LIGHT_WEAPON:
                InventoryManager.instance.AddNewLightWeapon(itemReward);
                break;
            case ItemBaseScript.ItemType.HEAVY_WEAPON:
                InventoryManager.instance.AddNewHeavySword(itemReward);
                break;
        }
        CloseChestReward();
    }
}