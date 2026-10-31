using UnityEngine;

public class PlayerRam : MonoBehaviour
{
    public float ramForce = 20f;
    public AudioClip hitSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = collision.transform.position - transform.position;
                direction.y = 0f;

                rb.AddForce(direction.normalized * ramForce, ForceMode.Impulse);
            }

            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }
    }
}