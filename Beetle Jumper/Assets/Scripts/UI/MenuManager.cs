using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject playerHUDPanel;
    public GameObject pausePanel;
    public GameObject instructionsPanel;
    public GameObject gameOverPanel;
    public GameObject endLevelPanel;

    public BeetleManager beetleManager;

    public bool isPaused;

    public void NextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void Instructions()
    {
        pausePanel.SetActive(false);

        instructionsPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        instructionsPanel.SetActive(false);
        playerHUDPanel.SetActive(false);

        pausePanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);

        playerHUDPanel.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void FinishLevel()
    {
        playerHUDPanel.SetActive(false);
        endLevelPanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void GameOver()
    {
        if (beetleManager != null && beetleManager.currentHealth <= 0)
        {
            playerHUDPanel.SetActive(false);

            gameOverPanel.SetActive(true);

            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    void Start()
    {
        playerHUDPanel.SetActive(true);

        instructionsPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        endLevelPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape");
            if (isPaused)
            {
                Resume();
                Debug.Log("resumed)");
            }
            else
            {
                Pause();
                Debug.Log("paused");
            }
        }

        GameOver();
    }
}
