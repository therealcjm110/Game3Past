using UnityEngine;

public class Tripwire : MonoBehaviour
{
    public GameObject dartPrefab;
    public Transform shootPoint; // Position and Direction the dart fires from
    public float fireDelay = 0.2f;
    private bool spent = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !spent)
        {
            spent = true; // One-time use per "life"
            Invoke(nameof(Shoot), fireDelay);
        }
    }

    void Shoot()
    {
        Instantiate(dartPrefab, shootPoint.position, shootPoint.rotation);
    }

    // Optional: Reset tripwire when player respawns
    public void ResetTrap() => spent = false;
}