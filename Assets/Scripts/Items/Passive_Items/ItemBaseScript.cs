using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName ="Resources/Items/NewItem")]
public class ItemBaseScript : ScriptableObject
{
    [Header("All Items Stats")]
    public string m_ItemName;
    public float m_ExtraHealth = 0;
    public float m_AttackSpeedMultiplier = 1;
    public float m_DamageDivider = 1;
    public float m_AbilityCooldownReduction = 1;  
    public float m_AttackDamageMultiplier = 1;
    public float m_GoldRewardMultiplier = 1;
    public float m_LifeSteal = 0; // Starts at 0, is multiplied by the amount of damage you do, you heal that ammount

    public int m_UnlockPrice = 0;
    public int m_Price;
    public string m_Description;
    public Sprite m_SpriteItem;
    public ItemQuality m_QualityItem = ItemQuality.NONE;
    public ItemType m_TypeItem = ItemType.NONE;
    public bool m_Unlocked = true;

    public enum ItemQuality { NONE = -1, COMMON, RARE, EPIC, LEGENDARY }
    public enum ItemType { NONE = -1, PASSIVE, ACTIVE, LIGHT_WEAPON, HEAVY_WEAPON }
    
    // Maybe change in the future
    [Header("Combat Stats")]
    public float m_ItemDamage;
    public float m_ItemCooldown;

    public float[] GetExtraAttributes()
    {
        float[] array = { m_ExtraHealth, m_AttackSpeedMultiplier, m_DamageDivider, m_AbilityCooldownReduction, 
            m_AttackDamageMultiplier, m_GoldRewardMultiplier, m_LifeSteal, (int)m_TypeItem};
        return array;
    }
}
