using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Timer Settings")]
    public TextMeshProUGUI timerText;
    public float timeRemaining = 300f;

    [Header("Flash Settings")]
    public float warningThreshold = 10f; // Time in seconds when flashing starts
    public Color normalColor = Color.white;
    public Color warningColor = Color.red;
    public float flashSpeed = 5f; // Higher = faster flashing

    [Header("Speedometer Settings")]
    public TextMeshProUGUI speedText;
    public Rigidbody playerRb;

    private float smoothSpeed;

    void Update()
    {
        // Stop timer if game is paused
        if (Time.timeScale == 0) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayPreciseTime(timeRemaining);
            HandleTimerColor(); // New function for the flashing effect
        }
        else
        {
            timeRemaining = 0;
            timerText.color = warningColor; // Keep it red at zero
            GameStateController.instance.TriggerGameOver();
        }

        // Speedometer Logic
        if (playerRb != null)
        {
            float currentSpeed = playerRb.linearVelocity.magnitude;
            smoothSpeed = Mathf.Lerp(smoothSpeed, currentSpeed, 10f * Time.deltaTime);
            speedText.text = Mathf.RoundToInt(smoothSpeed).ToString() + " MPH";
        }
    }

    void HandleTimerColor()
    {
        if (timeRemaining <= warningThreshold)
        {
            // Sin waves are great for flashing. It moves between 0 and 1.
            float lerp = Mathf.PingPong(Time.time * flashSpeed, 1.0f);
            timerText.color = Color.Lerp(normalColor, warningColor, lerp);
        }
        else
        {
            timerText.color = normalColor;
        }
    }

    void DisplayPreciseTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int milliSeconds = Mathf.FloorToInt((timeToDisplay % 1) * 100);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
    }
}