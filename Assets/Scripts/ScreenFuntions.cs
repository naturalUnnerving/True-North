using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFuntions : MonoBehaviour
{
    [SerializeField] private string newGameScene = "Main";
    [SerializeField] private string optionsScene = "OptionsScreen";
    [SerializeField] private string titleScreenScene = "TitleScreen";

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }
    public void Options()
    {
        SceneManager.LoadScene(optionsScene);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(titleScreenScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseToggle(PauseController pauseController)
    {
        pauseController.pauseMenu.SetActive(!pauseController.isPaused);
        Time.timeScale = pauseController.isPaused ? 0 : 1;
        pauseController.isPaused = !pauseController.isPaused;
    }
}
