using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public static RunnerManager instance;
    public Transform m_RunnerPlayerPos = null;

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

        RoomTransitionManager.instance.CallThisWhenPlayerDiesInCombat();

        FindAnyObjectByType<DeadMenuController>().m_HasSpawnedUsingVideo = false;

    }
}
