using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CurrentState : MonoBehaviour
{
    [SerializeField] private int Lives;
    [SerializeField] private int Coins;
    [SerializeField] private float Time;
    [SerializeField] private LootManager lootmanager;
    [SerializeField] private Timercontroller timer;
    [SerializeField] private TextMeshProUGUI coinsCount;

    [Header("Configuración de cambio de escena")]
    [SerializeField] private string SceneWhenLivesEnd = "Level 4";
    [SerializeField] private string SceneWhenTimeEnds = "Level 3";

    private bool isChangingScene = false;

    void Update()
    {
        this.Lives = lootmanager.NumberofLives;
        this.Time = timer.TimeRemaining;

        Coin();

        if (Lives <= 0 && !isChangingScene)
        {
            isChangingScene = true;
            StartCoroutine(DelayedSceneChange(SceneWhenLivesEnd));
        }

        if (Time <= 0 && !isChangingScene)
        {
            isChangingScene = true;
            StartCoroutine(DelayedSceneChange(SceneWhenTimeEnds));
        }
    }

    public void Coin()
    {
        PlayerPrefs.SetInt("CurrentState", lootmanager.NumberofCoins);
        Debug.Log(lootmanager.NumberofCoins);
        coinsCount.text = lootmanager.NumberofCoins.ToString();
    }

    private IEnumerator DelayedSceneChange(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
