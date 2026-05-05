using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform currentCheckpoint;
    public GameObject player;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (currentCheckpoint != null)
            StartCoroutine(InitialSpawn());
    }

    private IEnumerator InitialSpawn()
    {
        // Wait two frames to ensure all scripts (like PlayerMovement) 
        // have finished their internal setup.
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();

        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        if (currentCheckpoint == null || player == null) return;

        Rigidbody rb = player.GetComponent<Rigidbody>();

        // 1. Disable physics to prevent 'interpolation' fighting the move
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 2. Move the player
        player.transform.position = currentCheckpoint.position;
        player.transform.rotation = currentCheckpoint.rotation;

        // 3. Re-enable physics after the move
        if (rb != null)
            rb.isKinematic = false;

        Debug.Log("Forced Respawn to: " + currentCheckpoint.name);
    }
}