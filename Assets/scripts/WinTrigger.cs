using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 1. Use your working tag check (checks self and parent)
        bool hitPlayer = other.CompareTag("Player") ||
                         (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        // 2. Only trigger if it's the player AND the game is actually running
        if (hitPlayer && Time.timeScale > 0)
        {
            Debug.Log("<color=green>WIN CONDITION MET:</color> Player touched " + gameObject.name);

            if (GameStateController.instance != null)
            {
                // 3. Find the HUD to capture the final time remaining
                HUDController hud = Object.FindFirstObjectByType<HUDController>();

                if (hud != null)
                {
                    GameStateController.instance.TriggerWin(hud.timeRemaining);
                }
                else
                {
                    Debug.LogWarning("Win triggered but no HUDController found to get time!");
                    GameStateController.instance.TriggerWin(0); // Fallback
                }
            }
            else
            {
                Debug.LogError("FATAL: GameStateController instance not found!");
            }
        }
    }
}