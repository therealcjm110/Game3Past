using UnityEngine;

public class KillFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the object that entered the trigger is the player
        // (Checking parent in case a child collider like 'Feet' hits the floor)
        bool isPlayer = other.CompareTag("Player") ||
                       (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (isPlayer)
        {
            // 2. Double check the GameManager exists to avoid errors
            if (GameManager.instance != null)
            {
                Debug.Log("Player fell out of bounds. Respawning...");
                GameManager.instance.RespawnPlayer();
            }
            else
            {
                Debug.LogError("KillFloor: No GameManager instance found in the scene!");
            }
        }
    }

    // Optional: Use this if your kill floor is a SOLID object (not a trigger)
    private void OnCollisionEnter(Collision collision)
    {
        bool isPlayer = collision.gameObject.CompareTag("Player") ||
                       (collision.transform.parent != null && collision.transform.parent.CompareTag("Player"));

        if (isPlayer && GameManager.instance != null)
        {
            GameManager.instance.RespawnPlayer();
        }
    }
}