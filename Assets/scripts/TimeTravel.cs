using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [Header("Settings")]
    public float teleportDistance = 5f;
    public KeyCode teleportKey = KeyCode.E;

    [Header("Cooldown")]
    public float cooldownTime = 0.5f;
    private float nextReadyTime;

    private Rigidbody rb;
    private bool teleportedUp = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextReadyTime = 0f;
    }

    private void Update()
    {
        if (Time.time < nextReadyTime) return;

        if (Input.GetKeyDown(teleportKey))
        {
            nextReadyTime = Time.time + cooldownTime;
            ExecuteTeleport();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.RespawnPlayer();
        }
    }

    private void ExecuteTeleport()
    {

        if (!teleportedUp)
        {
            MovePlayer(Vector3.up * teleportDistance);
            teleportedUp = true;
            Debug.Log("Teleported UP");
        }
        else
        {
            MovePlayer(Vector3.down * teleportDistance);
            teleportedUp = false;
            Debug.Log("Teleported DOWN");
        }
    }

    private void MovePlayer(Vector3 displacement)
    {
        if (rb != null)
        {
            // rb.MovePosition is 'smoother' for objects with velocity
            rb.MovePosition(rb.position + displacement);
        }
        else
        {
            // Fallback if Rigidbody is missing
            transform.position += displacement;
        }
    }
    public void ResetTeleportState()
    {
        teleportedUp = false;
        nextReadyTime = 0f;
    }
}