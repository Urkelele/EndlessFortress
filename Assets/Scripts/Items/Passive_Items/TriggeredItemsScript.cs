using UnityEngine;

public class TriggeredItemsScript : ItemBaseScript
{
    [Header("Triggered params")]
    public TriggerType TriggerType = TriggerType.NONE;

    public virtual void OnTriggerActivated()
    {

    }
}
public enum TriggerType
{
    NONE = -1,
    ENEMY_DEATH,
    PLAYER_DEATH,
}
