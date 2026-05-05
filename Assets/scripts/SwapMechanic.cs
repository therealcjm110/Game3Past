using UnityEngine;
using UnityEngine.UI;

public class SwapMechanic : MonoBehaviour
{
    [Header("UI Elements")]
    public Image cooldownOverlay;
    public Image colorDisplay;

    [Header("Swap Settings")]
    public Sprite[] colorSprites;
    public float swapCooldown = 2f;
    public KeyCode swapKey = KeyCode.E;

    private int currentIndex = 0;
    private float lastSwapTime;
    private bool isReady = true;

    void Start()
    {
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
        if (Input.GetKeyDown(swapKey) && isReady)
        {
            TriggerSwap();
        }

        if (!isReady)
        {
            float timePassed = Time.time - lastSwapTime;
            float cooldownProgress = timePassed / swapCooldown;

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
        if (colorSprites.Length > 0)
        {
            currentIndex = (currentIndex + 1) % colorSprites.Length;
            colorDisplay.sprite = colorSprites[currentIndex];
        }

        lastSwapTime = Time.time;
        isReady = false;
        cooldownOverlay.fillAmount = 1;
    }

    // Call this from GameManager.RespawnPlayer()
    public void ResetUI()
    {
        isReady = true;
        currentIndex = 0;

        if (colorDisplay != null && colorSprites.Length > 0)
            colorDisplay.sprite = colorSprites[0];

        if (cooldownOverlay != null)
            cooldownOverlay.fillAmount = 0;

        Debug.Log("UI Swap Mechanic Reset!");
    }
}