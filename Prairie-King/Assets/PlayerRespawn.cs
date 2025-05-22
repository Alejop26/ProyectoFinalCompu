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

    [Header("Tiempo para reactivar collider")]
    public float colliderReactivateDelay = 10f;

    [Header("Posición de respawn (opcional)")]
    public Transform respawnPoint;

    [Header("Spawners a reactivar")]
    public MonoBehaviour[] spawners;

    [Header("GameObjects visuales")]
    public GameObject leg;

    [Header("Parpadeo")]
    public SpriteBlink spriteBlink;          // Referencia al componente de parpadeo
    public float blinkDuration = 3f;         // Duración del parpadeo después del respawn

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

        // Desactivar el SpriteRenderer de todos los GameObjects con el tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObj in players)
        {
            SpriteRenderer sr = playerObj.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = false;  // Desactivar el sprite
            }
        }

        // Reactivar funcionalidad (solo del jugador actual, que es este script)
        playerMovement.enabled = true;
        gun.enabled = true;

        // Desactivar collider al reaparecer para inmunidad temporal
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
            StartCoroutine(ReactivateColliderAfterDelay());
        }

        // Activar animación de respawn
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

        // Iniciar parpadeo y esperar que termine
        if (spriteBlink != null)
        {
            yield return StartCoroutine(spriteBlink.BlinkForDuration(blinkDuration));
        }

        // Esperar 2 segundos antes de reactivar el SpriteRenderer
        yield return new WaitForSeconds(2f);

        // Reactivar el SpriteRenderer de todos los GameObjects con el tag "Player"
        foreach (GameObject playerObj in players)
        {
            SpriteRenderer sr = playerObj.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = true;  // Reactivar el sprite
            }
        }

        // Al finalizar, forzar animación Idle (reemplaza "Idle" con el nombre correcto)
        animator.Play("Idle", -1, 0f);

        // Simular un paso adelante (como si el jugador presionara la tecla 'W')
        Vector2 forwardMovement = new Vector2(0, 1) * 0.5f;  // Simula un pequeño paso adelante (ajusta el valor según sea necesario)
        playerMovement.PlayerBody.MovePosition(playerMovement.PlayerBody.position + forwardMovement);

        Debug.Log("El jugador ha reaparecido, se ha dado un paso adelante y todos los objetos con tag 'Player' se han activado");
    }

    private IEnumerator ReactivateColliderAfterDelay()
    {
        yield return new WaitForSeconds(colliderReactivateDelay);
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
            Debug.Log("Collider reactivado después de inmunidad");
        }
    }
}
