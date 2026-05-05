using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Use the same robust check from your checkpoint code
        bool isPlayer = other.CompareTag("Player") ||
                       (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (isPlayer)
        {
            Debug.Log("Player touched spikes! Respawning...");
            GameManager.instance.RespawnPlayer();
        }
    }
}