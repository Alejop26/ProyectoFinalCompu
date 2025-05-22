using UnityEngine;



public class DestroyOnContact : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeDestroy = 3f; // Tiempo en segundos, editable desde Inspector

    private bool destructionScheduled = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ScheduleDestruction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ScheduleDestruction();
    }

    private void ScheduleDestruction()
    {
        if (!destructionScheduled)
        {
            destructionScheduled = true;
            Destroy(gameObject, delayBeforeDestroy);
        }
    }
}
