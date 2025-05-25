using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public static RunnerManager instance;

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
    public void RestartRun()
    {
        PlayerStats.instance.ResetValues();
        FindAnyObjectByType<PlayerHealthController>().RestartLife();
        EndlessRunnerTileManager.Instance.ControlRunner(true);
        InventoryManager.instance.RestartInventory();
        //InventoryManager.instance.UpdateButtons();
        FindAnyObjectByType<DeadMenuController>().m_HasSpawnedUsingVideo = false;
    }
}
