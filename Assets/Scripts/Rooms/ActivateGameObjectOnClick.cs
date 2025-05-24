using UnityEngine;

public class ActivateGameObjectOnClick : ClickableObject
{
    public GameObject m_GameObjectToActivate = null;
    
    protected override void OnClick()
    {
        m_GameObjectToActivate.SetActive(true);
    }
}
