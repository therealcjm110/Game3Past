using UnityEngine;

public class TimeGem : MonoBehaviour
{
    [Header("Settings")]
    public float timeToAdd = 10f;
    public float rotationSpeed = 100f;

    [Header("References")]
    private HUDController hud;

    void Start()
    {
        hud = Object.FindAnyObjectByType<HUDController>();
    }

    void Update()
    {
        // Spin the gem on the Y axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.CompareTag("Player") ||
                       (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (isPlayer && hud != null)
        {
            // Add the time and destroy the gem
            hud.timeRemaining += timeToAdd;
            Debug.Log("Gem Collected! Added " + timeToAdd + " seconds.");
            Destroy(gameObject);
        }
    }
}