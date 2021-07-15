using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public int totalCoins;
    public Text coinText;
    public int apples;
    public Text applesText;
    public Image heltBar;
    public Text scoreText;
    public GameObject gameOver;
    
    public int totalScore;
    public static GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreText();
    }

    public void AddCoin()
    {
        totalCoins++;
        coinText.text = totalCoins.ToString();
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void AddApple()
    {
        apples++;
    }

    public void PerderVida(float valr)
    {
        heltBar.fillAmount = valr / 10;
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
}
