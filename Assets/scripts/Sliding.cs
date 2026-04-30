using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovementAdvanced pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    public float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementAdvanced>();

        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Check if the slide key is pressed
        if (Input.GetKeyDown(slideKey))
        {
            // Only slide if the player is currently sprinting and moving
            if (pm.state == PlayerMovementAdvanced.MovementState.sprinting && (horizontalInput != 0 || verticalInput != 0))
            {
                StartSlide();
            }
        }

        // Stop slide logic 
        if (pm.sliding && (Input.GetKeyUp(slideKey) || slideTimer <= 0))
        {
            StopSlide();
        }
    }

    private void FixedUpdate()
    {
        if (pm.sliding)
            SlidingMovement();
    }

    private void StartSlide()
    {
        pm.sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        // Calculate direction and apply a ONE-TIME burst of speed
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // If player isn't pressing keys, slide forward based on orientation
        if (horizontalInput == 0 && verticalInput == 0)
            rb.AddForce(orientation.forward * slideForce, ForceMode.Impulse);
        else
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Normal sliding (non-slope)
        if (!pm.OnSlope() || rb.linearVelocity.y > -0.1f)
        {
            // We don't add force here anymore! 
            // Just let the StartSlide impulse carry us.

            slideTimer -= Time.deltaTime;
        }
        // Sliding down a slope
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        // Force an end if the timer runs out
        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        // Check if it's safe to stand up
        if (pm.CheckForCeiling()) return;

        pm.sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}
