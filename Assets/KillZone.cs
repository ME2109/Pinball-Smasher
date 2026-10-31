using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour
{
    private ScoreController scoreController;
    private GameManager gameManager;

    void Start()
    {
        scoreController = FindFirstObjectByType<ScoreController>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();

        if (enemy != null && enemy.enabled)
        {
            if (scoreController != null)
                scoreController.AddScore();

            StartCoroutine(EnemyDeath(enemy));
            return;
        }

        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            if (gameManager != null)
                gameManager.GameOver();

            Destroy(other.gameObject);
        }
    }

    private IEnumerator EnemyDeath(EnemyAI enemy)
    {
        enemy.enabled = false;

        Rigidbody rb = enemy.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        Animator animator = enemy.GetComponentInChildren<Animator>();

        if (animator != null)
            animator.SetTrigger("Death");

        yield return new WaitForSeconds(1.2f);

        if (enemy == null)
            yield break;

        if (animator != null)
        {
            Transform robotModel = animator.transform;
            Vector3 originalScale = robotModel.localScale;

            float duration = 0.6f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float progress = Mathf.Clamp01(elapsed / duration);

                robotModel.localScale =
                    Vector3.Lerp(originalScale, Vector3.zero, progress);

                yield return null;
            }

            robotModel.localScale = Vector3.zero;
        }

        Destroy(enemy.gameObject);
    }
}