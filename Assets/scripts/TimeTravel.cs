using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [Header("Settings")]
    public float teleportDistance = 5f;
    public KeyCode teleportKey = KeyCode.E;
    private float cooldownTimer;
    public float cooldownTime = 2f;
    private Rigidbody rb;
    private PlayerMovementAdvanced pm;

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
            TeleportUp();
            cooldownTimer = cooldownTime;
        }
    }

    private void TeleportUp()
    {

        transform.position += Vector3.up * teleportDistance;


        Debug.Log("Teleported Upwards!");
    }
}