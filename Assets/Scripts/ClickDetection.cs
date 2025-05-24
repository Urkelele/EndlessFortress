using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    //Turns on when the last click detected was to this object
    public bool m_IsLastObjectClicked;
    private void Update()
    {
        if(ClickManager.instance.m_LastObjectClicked == this) m_IsLastObjectClicked = true;
        else m_IsLastObjectClicked = false;
    }
}
