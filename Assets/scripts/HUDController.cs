using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Timer Settings")]
    public TextMeshProUGUI timerText;
    public float timeRemaining = 300f;

    [Header("Speedometer Settings")]
    public TextMeshProUGUI speedText;
    public Rigidbody playerRb;

    private float smoothSpeed; // Used to stop the 'spasming'

    void Update()
    {
        // 1. Precise Countdown Timer
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayPreciseTime(timeRemaining);
        }

        // 2. Smooth Speedometer
        if (playerRb != null)
        {
            float currentSpeed = playerRb.linearVelocity.magnitude;

            // Smooth the speed over time (0.1f controls the 'laziness' of the needle)
            smoothSpeed = Mathf.Lerp(smoothSpeed, currentSpeed, 10f * Time.deltaTime);

            speedText.text = Mathf.RoundToInt(smoothSpeed).ToString() + " MPH";
        }
    }

    void DisplayPreciseTime(float timeToDisplay)
    {
        if (timeToDisplay < 0) timeToDisplay = 0;

        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        // Multiply the remainder by 100 to get a 2-digit millisecond value
        int milliSeconds = Mathf.FloorToInt((timeToDisplay % 1) * 100);

        // Format: 00:00:00
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
    }
}