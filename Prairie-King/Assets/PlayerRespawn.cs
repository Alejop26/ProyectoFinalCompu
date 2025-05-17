using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Componentes del jugador")]
    public PlayerMovement playerMovement;
    public Gun gun;
    public Collider2D playerCollider;
    public Animator animator;
    public AudioSource audioSource;
    public HealthController healthController;

    [Header("Tiempo para reaparecer")]
    public float respawnDelay = 3f;

    [Header("Posición de respawn (opcional)")]
    public Transform respawnPoint;

    [Header("Spawners a reactivar")]
    public MonoBehaviour[] spawners;

    [Header("GameObjects visuales")]
    public GameObject leg;

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        // Reposicionar al jugador
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }

        // Reactivar funcionalidad
        playerMovement.enabled = true;
        gun.enabled = true;
        playerCollider.enabled = true;
        animator.SetTrigger("Respawn");
        if (audioSource != null) audioSource.Play();

        // Reactivar el objeto "Leg"
        if (leg != null)
            leg.SetActive(true);

        // Reactivar spawners
        foreach (MonoBehaviour spawner in spawners)
        {
            if (spawner != null)
                spawner.enabled = true;
        }

        // Restaurar vida
        if (healthController != null)
            healthController.Health = 1f;

        Debug.Log("El jugador ha reaparecido");
    }
}
