using UnityEngine;

public class Blowdart : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;

    void Start() => Destroy(gameObject, lifeTime);

    void Update() => transform.Translate(Vector3.forward * speed * Time.deltaTime);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.RespawnPlayer();
        }
        Destroy(gameObject);
    }
}