using UnityEngine;

public class ItemFunctionality : MonoBehaviour
{
    [SerializeField] ItemBaseScript ItemScript;
    public GameObject ItemManager;

    public bool m_ActivatedShownObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(ItemScript.m_TypeItem == ItemBaseScript.ItemType.NONE)
        {
            Debug.Log("The item: " + ItemScript.name + " has no type");
        }
        if (ItemScript.m_QualityItem == ItemBaseScript.ItemQuality.NONE)
        {
            Debug.Log("The item: " + ItemScript.name + " has no quality");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            if (!ItemManager.GetComponent<InventoryManager>().InsertItemInParent(ItemScript.m_TypeItem, transform))
            {
                Debug.Log("The item: " + ItemScript.name + " has no type");
            }
        }

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
