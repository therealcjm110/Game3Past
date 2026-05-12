using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance; // Added singleton for easy access

    [Header("Timer Settings")]
    public TextMeshProUGUI timerText;
    public float timeRemaining = 300f;

    [Header("Flash Settings")]
    public float warningThreshold = 10f;
    public Color normalColor = Color.white;
    public Color warningColor = Color.red;
    public float flashSpeed = 5f;

    [Header("Speedometer Settings")]
    public TextMeshProUGUI speedText;
    public Rigidbody playerRb;

    private float smoothSpeed;
    private bool timerActive = true;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        // 1. Stop timer logic if game is paused or timer is deactivated
        if (Time.timeScale == 0 || !timerActive) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayPreciseTime(timeRemaining);
            HandleTimerColor();
        }
        else
        {
            timeRemaining = 0;
            timerActive = false;
            timerText.color = warningColor;

            // 2. Trigger Game Over in the controller
            if (GameStateController.instance != null)
                GameStateController.instance.TriggerGameOver();
        }

        // Speedometer Logic
        if (playerRb != null)
        {
            // Note: If you are on an older Unity version, use .velocity instead of .linearVelocity
            float currentSpeed = playerRb.linearVelocity.magnitude;
            smoothSpeed = Mathf.Lerp(smoothSpeed, currentSpeed, 10f * Time.deltaTime);
            speedText.text = Mathf.RoundToInt(smoothSpeed * 2.237f).ToString() + " MPH"; // 2.237 converts m/s to MPH
        }
    }

    // Call this function from your Win Trigger
    public void StopTimer()
    {
        timerActive = false;
        if (GameStateController.instance != null)
        {
            GameStateController.instance.TriggerWin(timeRemaining);
        }
    }

    void HandleTimerColor()
    {
        if (timeRemaining <= warningThreshold)
        {
            float lerp = Mathf.PingPong(Time.unscaledTime * flashSpeed, 1.0f);
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