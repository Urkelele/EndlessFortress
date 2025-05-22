using UnityEngine;

public class ItemFunctionality : MonoBehaviour
{
    [SerializeField] ItemBaseScript ItemScript;
    public GameObject ItemManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) { transform.parent = ItemManager.transform; }
    }

    public float[] GetExtraAttributes()
    {
        return ItemScript.GetExtraAttributes();
    }
}
