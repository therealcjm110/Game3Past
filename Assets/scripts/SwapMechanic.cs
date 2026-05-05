using UnityEngine;
using UnityEngine.UI;

public class SwapMechanic : MonoBehaviour
{
    [Header("UI Elements")]
    public Image cooldownOverlay; // The semi-transparent image that fills up
    public Image colorDisplay;    // The image showing the current color

    [Header("Swap Settings")]
    public Sprite[] colorSprites; // Drag your color images here
    public float swapCooldown = 2f;
    public KeyCode swapKey = KeyCode.E; // Matches your Teleport key

    private int currentIndex = 0;
    private float lastSwapTime;
    private bool isReady = true;

    void Start()
    {
        // Set the initial color if images are provided
        if (colorSprites.Length > 0 && colorDisplay != null)
        {
            colorDisplay.sprite = colorSprites[0];
        }

        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 0;
        }
    }

    void Update()
    {
        // Check if the swap key is pressed and cooldown is finished
        if (Input.GetKeyDown(swapKey) && isReady)
        {
            TriggerSwap();
        }

        // Handle the visual cooldown fill
        if (!isReady)
        {
            float timePassed = Time.time - lastSwapTime;
            float cooldownProgress = timePassed / swapCooldown;

            // This slowly empties the 'overlay' as the timer resets
            cooldownOverlay.fillAmount = 1 - cooldownProgress;

            if (cooldownProgress >= 1)
            {
                isReady = true;
                cooldownOverlay.fillAmount = 0;
            }
        }
    }

    void TriggerSwap()
    {
        // 1. Change the Sprite
        if (colorSprites.Length > 0)
        {
            currentIndex = (currentIndex + 1) % colorSprites.Length;
            colorDisplay.sprite = colorSprites[currentIndex];
        }

        // 2. Start Cooldown
        lastSwapTime = Time.time;
        isReady = false;
        cooldownOverlay.fillAmount = 1;

        Debug.Log("UI: Swapped to color index " + currentIndex);
    }
}