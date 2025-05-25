using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public bool m_StopTime = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
}
