using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private bool m_Done = false;
    protected virtual void Update()
    {
        //Check if this object has been clicked and do the method once, when the object is not the last one clicked it gets ready again to do the method
        if(ClickManager.instance.m_LastObjectClicked == GetComponent<ClickDetection>() && !m_Done)
        {
            m_Done = true;
            OnClick();
        }
        else if(ClickManager.instance.m_LastObjectClicked != GetComponent<ClickDetection>() && m_Done)
        {
            m_Done = false;
        }
    }

    protected virtual void OnClick()
    {

    }
}
