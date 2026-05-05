using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    [Header("Settings")]
    public GameObject spikeVisual;
    public float warningDelay = 0.6f;
    public float moveSpeed = 8f;
    public float activeTime = 1.2f;

    private Vector3 hiddenPos;
    private Vector3 upPos;
    private bool isTriggered = false;
    private Collider spikeTrigger;

    void Start()
    {
        if (spikeVisual == null) return;

        hiddenPos = spikeVisual.transform.localPosition;


        upPos = hiddenPos + new Vector3(0, 2f, 0);

        spikeTrigger = spikeVisual.GetComponent<Collider>();
        if (spikeTrigger != null) spikeTrigger.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool hitPlayer = other.CompareTag("Player") ||
                        (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (hitPlayer && !isTriggered)
        {
            StartCoroutine(TriggerSpikes());
        }
    }

    IEnumerator TriggerSpikes()
    {
        isTriggered = true;

        // 1. Warning Delay
        yield return new WaitForSeconds(warningDelay);

        // Enable the kill trigger just before moving up
        if (spikeTrigger != null) spikeTrigger.enabled = true;

        // 2. Move Up
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            spikeVisual.transform.localPosition = Vector3.Lerp(hiddenPos, upPos, t);
            yield return null;
        }

        // 3. Stay Up
        yield return new WaitForSeconds(activeTime);

        // 4. Move Down
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            spikeVisual.transform.localPosition = Vector3.Lerp(upPos, hiddenPos, t);
            yield return null;
        }

        // Disable the kill trigger once they are hidden
        if (spikeTrigger != null) spikeTrigger.enabled = false;

        isTriggered = false;
    }
}