using UnityEngine;

public class BaseActiveScript : MonoBehaviour
{
    public float m_ActiveItemCooldown;

    public ItemBaseScript m_ItemBaseScript;
    
    public virtual bool UseActive()
    {
        if (m_ItemBaseScript.m_TypeItem != ItemBaseScript.ItemType.ACTIVE)
        {
            Debug.Log("The item: " + m_ItemBaseScript.m_ItemName + " has no ACTIVE parameter");
            return false;
        }
        return true;
    }
}
