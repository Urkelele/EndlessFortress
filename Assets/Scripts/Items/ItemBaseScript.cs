using UnityEngine;


[CreateAssetMenu(menuName ="Items/NewItem")]
public class ItemBaseScript : ScriptableObject
{
    public string m_ItemName;
    public float m_ExtraHealth;
    public float m_AttackSpeedMultiplier;
    public float m_DamageReductionMultiplier;
    public int m_Price;
    public string m_Description;
    public enum m_Quality { NONE, COMMON, RARE, EPIC, LEGENDARY};

    public float[] GetExtraAttributes()
    {
        float[] array = { m_ExtraHealth, m_AttackSpeedMultiplier, m_DamageReductionMultiplier };
        return array;
    }
}
