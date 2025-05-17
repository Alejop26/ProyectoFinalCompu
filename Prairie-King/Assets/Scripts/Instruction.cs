using System.Collections;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    [SerializeField] private float timeToHide = 5f; // Puedes cambiar esto en el Inspector

    private void Start()
    {
        StartCoroutine(TurnOff());
    }

    private IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(timeToHide);
        this.gameObject.SetActive(false);
    }
}
