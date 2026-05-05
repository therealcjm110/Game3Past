using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Debug line to see what is physically hitting the trigger
        Debug.Log("Collision detected with: " + other.name);

        // Check the object itself OR any of its parents for the "Player" tag
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            if (GameManager.instance != null)
            {
                // Update the checkpoint
                GameManager.instance.currentCheckpoint = this.transform;
                Debug.Log("<color=green>SUCCESS:</color> Checkpoint set to " + gameObject.name);
            }
            else
            {
                Debug.LogError("FATAL: No GameManager found in the scene!");
            }
        }
    }
}