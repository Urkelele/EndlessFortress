using UnityEngine;

public class SwipeDetectorScript : MonoBehaviour
{
    private Vector2 m_StartPosition;
    private bool m_IsSwiping;
    private float m_SwipeThreshold = 50f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouch(touch);
        }
    }

    void HandleTouch(Touch thisTouch)
    {
        if (!m_IsSwiping)
        {
            if (thisTouch.phase == TouchPhase.Began)
            {
                m_StartPosition = thisTouch.position;
                m_IsSwiping = true;
            }
            else if(thisTouch.phase == TouchPhase.Ended && m_IsSwiping)
            {
                Vector2 endPosition = thisTouch.position;
                Vector2 swipeDelta = endPosition - m_StartPosition;
                swipeDelta.y = 0;
                if(swipeDelta.magnitude > m_SwipeThreshold)
                {
                    if (endPosition.x > m_StartPosition.x)
                    {
                        Debug.Log("Derecha");
                    }
                    else
                    {
                        Debug.Log("Izquierda");
                    }


                }
            }
        }
       
    }
}
