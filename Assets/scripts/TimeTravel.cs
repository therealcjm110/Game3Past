using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [Header("Settings")]
    public float teleportDistance = 5f;
    public KeyCode teleportKey = KeyCode.E;

    [Header("Cooldown")]
    public float cooldownTime = 0.5f;
    private float nextReadyTime; // The exact timestamp when teleporting is allowed again

    private Rigidbody rb;
    private PlayerMovementAdvanced pm;
    private bool teleportedUp = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementAdvanced>();

        // Safety check: ensure we don't start locked
        nextReadyTime = 0f;
    }

    private void Update()
    {
        // Time.time is the current game time in seconds
        if (Input.GetKeyDown(teleportKey) && Time.time >= nextReadyTime)
        {
            ToggleTeleport();
        }
    }

    private void ToggleTeleport()
    {
        // Update the timestamp immediately
        // Current time + cooldown = the soonest we can do this again
        nextReadyTime = Time.time + cooldownTime;

        if (!teleportedUp)
        {
            transform.position += Vector3.up * teleportDistance;
            teleportedUp = true;
            Debug.Log("Teleported Up!");
        }
        else
        {
            transform.position += Vector3.down * teleportDistance;
            teleportedUp = false;
            Debug.Log("Teleported Down!");
        }
    }
}