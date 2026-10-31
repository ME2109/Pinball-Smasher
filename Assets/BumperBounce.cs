using UnityEngine;

public class BumperBounce : MonoBehaviour
{
    public float bounceForce = 15f;
    public AudioClip bounceSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction =
                collision.transform.position - transform.position;

            direction.y = 0f;

            rb.AddForce(
                direction.normalized * bounceForce,
                ForceMode.Impulse
            );

            if (bounceSound != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.2f);
                audioSource.PlayOneShot(bounceSound);
            }
        }
    }
}