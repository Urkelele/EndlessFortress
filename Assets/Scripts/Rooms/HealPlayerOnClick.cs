using UnityEngine;

public class HealPlayerOnClick : ClickableObject
{
    private PlayerHealthController m_PlayerHealthController;
    private void Awake()
    {
        m_PlayerHealthController = FindAnyObjectByType<PlayerHealthController>();
    }
    protected override void OnClick()
    {
        m_PlayerHealthController.m_CurrentHealthPoints = m_PlayerHealthController.m_MaxHealthPoints;
        GeneralCanvasManager.instance.ReturnToRunWithDelaySeconds(3);
    }

}
