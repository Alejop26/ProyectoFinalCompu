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

    private bool isChangingScene = false;

    void Update()
    {
        this.Lives = lootmanager.NumberofLives;
        this.Time = timer.TimeRemaining;

        Coin();

        // Cambiar a "Level 1" si se acaban las vidas
        if (Lives <= 0 && !isChangingScene)
        {
            isChangingScene = true;
            StartCoroutine(DelayedSceneChange("Level 1"));
        }

        // Cambiar a "Level 2" si se acaba el tiempo
        if (Time <= 0 && !isChangingScene)
        {
            isChangingScene = true;
            StartCoroutine(DelayedSceneChange("Level 2"));
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
