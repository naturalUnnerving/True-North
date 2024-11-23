using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFunctions : MonoBehaviour
{
    [SerializeField] private string newGameScene = "Main";
    [SerializeField] private string optionsScene = "OptionsScreen";
    [SerializeField] private string titleScreenScene = "TitleScreen";
    private static string previousScene = "";
    private void SetPreviousScene()
    {
        previousScene = SceneManager.GetActiveScene().name;
    }
    public void NewGame()
    {
        SetPreviousScene();
        SceneManager.LoadScene(newGameScene);
    }
    public void Options()
    {
        SetPreviousScene();
        SceneManager.LoadScene(optionsScene);
    }

    public void TitleScreen()
    {
        SetPreviousScene();
        SceneManager.LoadScene(titleScreenScene);
    }

    public void GoBack()
    {   
        Debug.Log(previousScene);
        SceneManager.LoadScene(previousScene);
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
