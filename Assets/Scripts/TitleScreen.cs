using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public string newGameScene;
    public string optionsScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }
    public void Options()
    {
        SceneManager.LoadScene(optionsScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
