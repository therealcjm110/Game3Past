using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // This stops the physics engine from ever tilting the player
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Calculate the target velocity
        Vector3 targetVelocity = move * speed;

        // Calculate the difference between current and target velocity
        Vector3 velocityChange = (targetVelocity - rb.linearVelocity);
        velocityChange.y = 0; // Don't touch the up/down speed!

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
