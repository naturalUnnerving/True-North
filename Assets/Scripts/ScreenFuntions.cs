using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFuntions : MonoBehaviour
{
    [SerializeField] private string newGameScene;
    [SerializeField] private string optionsScene;
    [SerializeField] private string titleScreenScene;

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
}
