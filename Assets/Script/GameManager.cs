using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int points = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pausebutton;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = points.ToString();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void updateScore(int score)
    {
        points += score;
        scoreText.text = points.ToString();
    }

    public void pauseButton()
    {
        pausebutton.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausebutton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Game is quit");
        Application.Quit();
    }
}
