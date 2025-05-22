using System.Collections;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    [Header("Parpadeo")]
    [SerializeField] private float blinkSpeed = 2f;     // Ciclos por segundo
    [SerializeField] private float blinkDuration = 3f;  // Duración total del parpadeo

    [Header("Referencias (asignar en el Inspector)")]
    [Tooltip("SpriteRenderer del GameObject principal (Player)")]
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [Tooltip("GameObject hijo 'Leg' que tiene su propio SpriteRenderer")]
    [SerializeField] private GameObject legObject;

    private SpriteRenderer legSpriteRenderer;
    private Color playerOriginalColor;
    private Color legOriginalColor;

    void Awake()
    {
        // Si no asignaste el SpriteRenderer del player, lo buscamos aquí
        if (playerSpriteRenderer == null)
            playerSpriteRenderer = GetComponent<SpriteRenderer>();

        if (playerSpriteRenderer != null)
            playerOriginalColor = playerSpriteRenderer.color;
        else
            Debug.LogError($"[{name}] No hay SpriteRenderer en Player asignado ni en el mismo GameObject.");

        // Preparar el SpriteRenderer de leg
        if (legObject != null)
        {
            legSpriteRenderer = legObject.GetComponent<SpriteRenderer>()
                ?? legObject.GetComponentInChildren<SpriteRenderer>();

            if (legSpriteRenderer != null)
                legOriginalColor = legSpriteRenderer.color;
            else
                Debug.LogError($"[{name}] No se encontró SpriteRenderer en '{legObject.name}'.");
        }
        else
        {
            Debug.LogWarning($"[{name}] No asignaste legObject en el Inspector.");
        }
    }

    // Llamar: StartCoroutine(spriteBlink.BlinkForDuration(blinkDuration));
    public IEnumerator BlinkForDuration(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float alpha = (Mathf.Sin(Time.time * blinkSpeed * Mathf.PI * 2f) + 1f) / 2f;

            if (playerSpriteRenderer != null)
            {
                var c = playerOriginalColor;
                c.a = alpha;
                playerSpriteRenderer.color = c;
            }

            if (legSpriteRenderer != null)
            {
                var cLeg = legOriginalColor;
                cLeg.a = alpha;
                legSpriteRenderer.color = cLeg;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restaurar colores originales
        if (playerSpriteRenderer != null)
            playerSpriteRenderer.color = playerOriginalColor;
        if (legSpriteRenderer != null)
            legSpriteRenderer.color = legOriginalColor;
    }
}
