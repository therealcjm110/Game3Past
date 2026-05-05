using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform currentCheckpoint;
    public GameObject player;

    private void Awake()
    {
        // Singleton pattern to make this easy to call from other scripts
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void RespawnPlayer()
    {
        if (currentCheckpoint != null)
        {
            player.transform.position = currentCheckpoint.position;
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = Vector3.zero;

            // Reset all traps in the scene
            foreach (Tripwire trap in FindObjectsByType<Tripwire>(FindObjectsSortMode.None))
            {
                trap.ResetTrap();
            }
        }
    }
}