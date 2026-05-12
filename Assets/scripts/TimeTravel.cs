using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [Header("Settings")]
    public float teleportDistance = 5f;
    // We keep this as a default, but we'll prioritize the GameStateController key
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
        // 1. Get the dynamic key from the GameStateController
        KeyCode activeKey = (GameStateController.instance != null)
            ? GameStateController.instance.GetTeleportKey()
            : teleportKey;

        // 2. Prevent teleporting if the game is paused (Time.timeScale == 0)
        // This is CRITICAL if the user binds Left Click to teleport.
        if (Time.timeScale == 0) return;

        if (Time.time < nextReadyTime) return;

        // 3. Execute teleport on key press
        if (Input.GetKeyDown(activeKey))
        {
            nextReadyTime = Time.time + cooldownTime;
            ExecuteTeleport();
        }

        // Emergency Respawn Key
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameManager.instance != null)
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
            // 1. Tell the physics engine we are manually moving the object
            rb.interpolation = RigidbodyInterpolation.None;

            // 2. Clear old velocity so the teleport is clean
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 3. Move the position
            rb.position += displacement;
            transform.position = rb.position; // Sync transform immediately

            // 4. Turn interpolation back on for smooth movement
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
        else
        {
            // Fallback if no Rigidbody exists
            transform.position += displacement;
        }
    }

    public void ResetTeleportState()
    {
        teleportedUp = false;
        nextReadyTime = 0f;
    }
}