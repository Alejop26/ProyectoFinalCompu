using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    [SerializeField] private HealthController healthcontroller;
    [SerializeField] private Instruction instruction;
    void Start()
    {
        
    }

    void Update()
    {
        if (healthcontroller.Health <= 0)
        {
            StartCoroutine(OnDeath());
        }  
    }

    private IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(3f);
        //escribimos en consola que el jugador ha muerto
        Debug.Log("El jugador ha muerto");
        
    }
}
