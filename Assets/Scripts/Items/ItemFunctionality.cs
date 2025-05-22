using UnityEngine;

public class ItemFunctionality : MonoBehaviour
{
    [SerializeField] ItemBaseScript ItemScript;
    public GameObject ItemManager;

    public bool m_ActivatedShownObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) { transform.parent = ItemManager.transform; }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //First element touching the screen always at index 0
            if (touch.phase == TouchPhase.Began)
            {
                Debug.LogError("touch");
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                //2D collisions
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                }


                //3D collisions
                RaycastHit hit3D;
                Physics.Raycast(ray.origin, ray.direction, out hit3D, Mathf.Infinity);
                if (hit3D.collider != null)
                {
                    Debug.Log(hit3D.collider.gameObject.name);
                    m_ActivatedShownObject = !m_ActivatedShownObject;
                }


            }
        }
        ItemStatsUI.ShowItemUI_Instance.ShowItemInfo(ItemScript, m_ActivatedShownObject);


    }

    public float[] GetExtraAttributes()
    {
        return ItemScript.GetExtraAttributes();
    }
}
