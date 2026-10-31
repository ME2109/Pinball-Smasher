using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-6f, 6f);
        float z = Random.Range(-6f, 6f);

        Vector3 spawnPosition = new Vector3(x, 1.5f, z);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}