using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFuntions : MonoBehaviour
{
    [SerializeField] private string newGameScene;
    [SerializeField] private string optionsScene;
    [SerializeField] private string titleScreenScene;
    
    [Header("Audio")]
    [SerializeField] private float volume = 1f;
    [SerializeField] private AudioRefSO audioRefSO;
    [SerializeField] private float buttonTimer = 2f;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button titleButton;
    [SerializeField] private Button quitButton;


    private void Awake() {
        if (startButton != null)
        {
            startButton.onClick.AddListener(() =>
            {
                //Click
                NewGame();
            });
        }

        if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(() =>
            {
                //Click
                Options();
            });
        }

        if (titleButton != null)
        {
            titleButton.onClick.AddListener(() =>
            {
                //Click
                TitleScreen();
            });
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(() =>
            {
                //Click
                QuitGame();
            });
        }
    }


    public void NewGame()
    {
        startButton.GetComponentInChildren<AudioSource>().PlayOneShot(audioRefSO.startClick, volume);
        StartCoroutine(ButtonTimerBeforeSceneSwitch(audioRefSO.startClick.length + 0.5f));
        SceneManager.LoadScene(newGameScene);
    }
    public void Options()
    {
        optionsButton.GetComponentInChildren<AudioSource>().PlayOneShot(audioRefSO.mouseClick, volume);
        StartCoroutine(ButtonTimerBeforeSceneSwitch(audioRefSO.mouseClick.length + 0.5f));
        SceneManager.LoadScene(optionsScene);
    }

    public void TitleScreen()
    {
        titleButton.GetComponentInChildren<AudioSource>().PlayOneShot(audioRefSO.mouseClick, volume);
        StartCoroutine(ButtonTimerBeforeSceneSwitch(audioRefSO.mouseClick.length + 0.5f));
        SceneManager.LoadScene(titleScreenScene);
    }

    public void QuitGame()
    {
        quitButton.GetComponentInChildren<AudioSource>().PlayOneShot(audioRefSO.mouseClick, volume);
        StartCoroutine(ButtonTimerBeforeSceneSwitch(audioRefSO.mouseClick.length + 0.5f));
        Application.Quit();
    }

    private IEnumerator ButtonTimerBeforeSceneSwitch(float length)
    {
        yield return new WaitForSeconds(length);
    }
}
