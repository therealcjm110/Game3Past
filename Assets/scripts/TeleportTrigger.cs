using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [Header("Teleport Settings")]
    [Tooltip("Drag the destination 'Empty GameObject' here.")]
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.CompareTag("Player") ||
                        (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (isPlayer && destination != null)
        {
            Transform playerRoot = other.CompareTag("Player") ? other.transform : other.transform.parent;

            playerRoot.position = destination.position;
            playerRoot.rotation = destination.rotation;

            Rigidbody rb = playerRoot.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            Debug.Log($"<color=cyan>Teleported {playerRoot.name} to {destination.name}</color>");
        }
    }
}