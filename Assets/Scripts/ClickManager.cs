using UnityEngine;
using UnityEngine.UIElements;

public class ClickManager : MonoBehaviour
{
    public static ClickManager instance;

    public ClickDetection m_LastObjectClicked = null;

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

    void Update()
    {
        // MOBILE INPUT
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //First element touching the screen always at index 0
            if (touch.phase == TouchPhase.Began)
            {
                ////Remove last object clicked
                //if (m_LastObjectClicked != null)
                //{
                //    m_LastObjectClicked.GetComponent<ClickDetection>().m_IsLastObjectClicked = false;
                //}

                //m_LastObjectClicked = null;

                //Ray ray = RoomTransitionManager.instance.m_CurrentActiveCamera.ScreenPointToRay(touch.position);

                ////2D collisions
                //RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                //if (hit2D.collider != null)
                //{
                //    //When a new object has been clicked, 
                //    Debug.Log("2D object: " + hit2D.collider.gameObject.name + " was touched");
                //    hit2D.collider.gameObject.GetComponent<ClickDetection>().m_IsLastObjectClicked = true;
                //    m_LastObjectClicked = hit2D.collider.gameObject.GetComponent<ClickDetection>();
                //}

                ////3D collisions
                //RaycastHit hit3D;
                //Physics.Raycast(ray.origin, ray.direction, out hit3D, Mathf.Infinity);
                //if (hit3D.collider != null && hit3D.collider.gameObject.GetComponent<ClickDetection>())
                //{
                //    Debug.Log("3D object: " + hit3D.collider.gameObject.name + " was touched");                    
                //    hit3D.collider.gameObject.GetComponent<ClickDetection>().m_IsLastObjectClicked = true;
                //    m_LastObjectClicked = hit3D.collider.gameObject.GetComponent<ClickDetection>();
                //}
                ProcessClick(touch.position);
            }
        }

        // PC INPUT
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            ProcessClick(Input.mousePosition);
        }
    }

    void ProcessClick(Vector3 position)
    {
        //Remove last object clicked
        if (m_LastObjectClicked != null)
        {
            m_LastObjectClicked.GetComponent<ClickDetection>().m_IsLastObjectClicked = false;
        }

        m_LastObjectClicked = null;

        Ray ray = RoomTransitionManager.instance.m_CurrentActiveCamera.ScreenPointToRay(position);

        //2D collisions
        RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit2D.collider != null)
        {
            //When a new object has been clicked, 
            Debug.Log("2D object: " + hit2D.collider.gameObject.name + " was touched");
            hit2D.collider.gameObject.GetComponent<ClickDetection>().m_IsLastObjectClicked = true;
            m_LastObjectClicked = hit2D.collider.gameObject.GetComponent<ClickDetection>();
        }

        //3D collisions
        RaycastHit hit3D;
        Physics.Raycast(ray.origin, ray.direction, out hit3D, Mathf.Infinity);
        if (hit3D.collider != null && hit3D.collider.gameObject.GetComponent<ClickDetection>())
        {
            Debug.Log("3D object: " + hit3D.collider.gameObject.name + " was touched");
            hit3D.collider.gameObject.GetComponent<ClickDetection>().m_IsLastObjectClicked = true;
            m_LastObjectClicked = hit3D.collider.gameObject.GetComponent<ClickDetection>();
        }
    }

}
