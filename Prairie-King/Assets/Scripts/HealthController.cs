using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public float Health;
    public Gun gun;
    public UnityEvent OnDied;

    [Tooltip("Si está activo, al morir se carga la escena de Créditos después de 3 segundos")]
    public bool loadCreditsOnDeath = false;

    private void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
    }

    public void TakeDamage()
    {
        if (Health == 0)
            return;

        Health -= gun.Damage;

        if (Health <= 0)
        {
            Health = 0;
            OnDied.Invoke();

            if (loadCreditsOnDeath)
                StartCoroutine(LoadCreditsAfterDelay(1f));
        }
    }

    public void TakeNuke()
    {
        Health -= 10;
        OnDied.Invoke();

        if (loadCreditsOnDeath && Health <= 0)
            StartCoroutine(LoadCreditsAfterDelay(1f));
    }

    private IEnumerator LoadCreditsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Creditos");
    }
}
