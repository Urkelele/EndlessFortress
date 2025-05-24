using UnityEngine;

public class TomePickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ExternalDataManager.Instance.AddTomes(1);
        }
    }
}
