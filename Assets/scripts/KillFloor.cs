using UnityEngine;

public class KillFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        bool isPlayer = other.CompareTag("Player") ||
                       (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (isPlayer)
        {
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