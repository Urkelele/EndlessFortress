using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName ="Items/NewItem")]
public class ItemBaseScript : ScriptableObject
{
    public string m_ItemName;
    public float m_ExtraHealth;
    public float m_AttackSpeedMultiplier;
    public float m_DamageReductionMultiplier;
    public int m_Price;
    public string m_Description;
    public Sprite m_SpriteItem;
    public ItemQuality m_QualityItem = ItemQuality.NONE;
    public ItemType m_TypeItem = ItemType.NONE;
    public enum ItemQuality { NONE = -1, COMMON, RARE, EPIC, LEGENDARY}
    public enum ItemType { NONE = -1,PASSIVE, ACTIVE, LIGHT_WEAPON, HEAVY_WEAPON}

    public float[] GetExtraAttributes()
    {
        float[] array = { m_ExtraHealth, m_AttackSpeedMultiplier, m_DamageReductionMultiplier,  (int)m_TypeItem};
        return array;
    }
}
