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
        FindAnyObjectByType<PlayerHealthController>().RestartLife();
        EndlessRunnerTileManager.Instance.ControlRunner(true);
        InventoryManager.instance.RestartInverntory();
        PlayerStats.instance.ResetValues();
        FindAnyObjectByType<DeadMenuController>().m_HasSpawnedUsingVideo = false;
    }
}
