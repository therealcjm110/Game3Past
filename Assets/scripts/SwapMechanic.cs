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

        KeyCode activeKey = (GameStateController.instance != null)
            ? GameStateController.instance.GetTeleportKey()
            : swapKey;

        if (Time.timeScale == 0) return;

        if (Input.GetKeyDown(activeKey) && isReady)
        {
            TriggerSwap();
        }

        if (!isReady)
        {
            float timePassed = Time.time - lastSwapTime;
            float cooldownProgress = timePassed / swapCooldown;

            if (cooldownOverlay != null)
                cooldownOverlay.fillAmount = 1 - cooldownProgress;

            if (cooldownProgress >= 1)
            {
                isReady = true;
                if (cooldownOverlay != null)
                    cooldownOverlay.fillAmount = 0;
            }
        }
    }

    void TriggerSwap()
    {
        if (colorSprites.Length > 0)
        {
            currentIndex = (currentIndex + 1) % colorSprites.Length;
            if (colorDisplay != null)
                colorDisplay.sprite = colorSprites[currentIndex];
        }

        lastSwapTime = Time.time;
        isReady = false;

        if (cooldownOverlay != null)
            cooldownOverlay.fillAmount = 1;
    }

    public void ResetUI()
    {
        isReady = true;
        currentIndex = 0;
        lastSwapTime = -swapCooldown; 

        if (colorDisplay != null && colorSprites.Length > 0)
            colorDisplay.sprite = colorSprites[0];

        if (cooldownOverlay != null)
            cooldownOverlay.fillAmount = 0;

        Debug.Log("UI Swap Mechanic Reset!");
    }
}