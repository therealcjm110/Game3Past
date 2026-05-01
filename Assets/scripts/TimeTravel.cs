using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [Header("Settings")]
    public float teleportDistance = 5f;
    public KeyCode teleportKey = KeyCode.E;

    [Header("Cooldown")]
    public float cooldownTime = 1f;
    private float cooldownTimer;

    private Rigidbody rb;
    private PlayerMovementAdvanced pm;

    private bool teleportedUp = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementAdvanced>();
    }

    private void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(teleportKey) && cooldownTimer <= 0)
        {
            ToggleTeleport();
            cooldownTimer = cooldownTime;
        }
    }

    private void ToggleTeleport()
    {
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