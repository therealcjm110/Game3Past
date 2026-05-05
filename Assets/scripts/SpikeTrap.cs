using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    [Header("Settings")]
    public Transform spikeVisual;
    public float delayBeforePopUp = 0.5f;
    public float activeTime = 1f;
    public float retractSpeed = 5f;

    private Vector3 hiddenPos;
    private Vector3 activePos;
    private bool isTriggered = false;

    void Start()
    {
        hiddenPos = spikeVisual.localPosition;
        activePos = hiddenPos + new Vector3(0, 1.5f, 0); // Adjust height as needed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            StartCoroutine(TriggerSpikes());
        }
    }

    IEnumerator TriggerSpikes()
    {
        isTriggered = true;

        // Wait for the player to keep running
        yield return new WaitForSeconds(delayBeforePopUp);

        // Snap spikes up!
        spikeVisual.localPosition = activePos;

        // Check if player is still touching it when it pops up
        // (You can add a small trigger here or use a simple distance check)

        yield return new WaitForSeconds(activeTime);

        // Retract slowly
        while (Vector3.Distance(spikeVisual.localPosition, hiddenPos) > 0.01f)
        {
            spikeVisual.localPosition = Vector3.Lerp(spikeVisual.localPosition, hiddenPos, Time.deltaTime * retractSpeed);
            yield return null;
        }

        isTriggered = false;
    }
}