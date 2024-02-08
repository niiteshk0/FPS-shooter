using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int points = 0;
    public TextMeshProUGUI scoreText;
    public GameObject pauseMenu;

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

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Game is quit");
        Application.Quit();
    }
}
