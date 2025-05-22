using System.Collections;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [Header("Balas")]
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    public float fireRate = 0.5f;             // Segundos entre disparos
    public float shootingDuration = 50f;      // Duración total disparando

    [Header("Murciélagos")]
    public GameObject batPrefab;
    public float batSpawnDuration = 50f;      // Duración total del spawneo de murciélagos
    public int batsPerWave = 3;
    public float waveInterval = 5f;

    [Header("Ciclos")]
    public int cycles = 3;                    // Cuántas veces repetir disparar + spawnear

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(CycleRoutine());
    }

    private IEnumerator CycleRoutine()
    {
        for (int i = 0; i < cycles; i++)
        {
            // Disparar balas por shootingDuration segundos
            yield return StartCoroutine(ShootForDuration());

            // Spawnear murciélagos por batSpawnDuration segundos
            yield return StartCoroutine(SpawnBatWaves());
        }
    }

    private IEnumerator ShootForDuration()
{
    yield return new WaitForSeconds(2f); // Espera inicial

    float timer = 0f;
    while (timer < shootingDuration)
    {
        ShootTriple();
        yield return new WaitForSeconds(fireRate);
        timer += fireRate;
    }
}


    private void ShootTriple()
    {
        float[] angles = { -15f, 0f, 15f };
        float spawnDistance = 1.0f;

        foreach (float angle in angles)
        {
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
            Vector2 spawnPos = (Vector2)transform.position + direction * spawnDistance;

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.Euler(0, 0, angle));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = direction * bulletForce;
        }
    }

    private IEnumerator SpawnBatWaves()
    {
        float elapsed = 0f;
        while (elapsed < batSpawnDuration)
        {
            SpawnBatWave();
            yield return new WaitForSeconds(waveInterval);
            elapsed += waveInterval;
        }
    }

    private void SpawnBatWave()
    {
        float spawnDistance = 2f;
        Vector2 centerPos = (Vector2)transform.position + (Vector2)(transform.up * spawnDistance);

        Vector2 center = centerPos;
        Vector2 right = centerPos + (Vector2)(transform.right * 1f) + (Vector2)(-transform.up * 0.5f);
        Vector2 left = centerPos + (Vector2)(-transform.right * 1f) + (Vector2)(-transform.up * 0.5f);

        Instantiate(batPrefab, center, Quaternion.identity);
        Instantiate(batPrefab, right, Quaternion.identity);
        Instantiate(batPrefab, left, Quaternion.identity);
    }
}
