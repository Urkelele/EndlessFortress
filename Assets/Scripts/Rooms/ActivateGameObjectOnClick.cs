using UnityEngine;
using static AbilityManager;

public class ActivateGameObjectOnClick : ClickableObject
{
    public enum RoomType
    {
        None = -1,
        Shop = 0,
        Chest = 1
    };

    [SerializeField] RoomType m_ChosenRoom = RoomType.None;

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnClick()
    {
        switch (m_ChosenRoom)
        {
            case RoomType.Chest:
                Debug.LogWarning("Chest Opened");
                GeneralCanvasManager.instance.OpenChest();
                break;
            case RoomType.Shop:
                Debug.LogWarning("Shop Entered");
                GeneralCanvasManager.instance.OpenShop();
                break;

            default:
                Debug.Log("Ability Not Selected");
                break;
        }
    }
}
