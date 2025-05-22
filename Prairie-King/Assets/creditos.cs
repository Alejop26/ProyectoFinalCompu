using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    [Header("Desplazamiento")]
    [Tooltip("Velocidad de desplazamiento hacia arriba (unidades de UI por segundo)")]
    public float scrollSpeed = 20f;

    [Tooltip("RectTransform del texto de créditos (si está vacío, usa el RectTransform de este GameObject)")]
    public RectTransform textRect;

    [Header("Música de fondo")]
    [Tooltip("AudioSource que reproducirá la canción (arrástralo desde el Inspector)")]
    public AudioSource audioSource;
    [Tooltip("¿Reproducir en bucle?")]
    public bool loopMusic = true;

    void Awake()
    {
        // Si no has asignado manualmente el Text, usa el RectTransform de este objeto
        if (textRect == null)
            textRect = GetComponent<RectTransform>();
    }

    void Start()
    {
        // Inicia la música de fondo si hay AudioSource
        if (audioSource != null)
        {
            audioSource.loop = loopMusic;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (textRect != null)
        {
            // Mueve el texto hacia arriba cada frame
            textRect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        }
    }
}
