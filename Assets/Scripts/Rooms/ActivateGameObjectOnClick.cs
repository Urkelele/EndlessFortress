using UnityEngine;

public class RoomObjectOnClick : ClickableObject
{
    public GameObject m_GameObjectToActivate = null;

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnClick()
    {
        m_GameObjectToActivate.SetActive(true);
    }
}
