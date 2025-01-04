using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject dogPanel;
    [SerializeField] GameObject playerIndicator;
    [SerializeField] GameObject dogIndicator;
    [SerializeField] GameObject bearIndicator;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPlayerPanel()
    {
        if (playerPanel.activeSelf)
            return;
        playerPanel.SetActive(true);
        dogPanel.SetActive(false);

        playerIndicator.SetActive(true);
        dogIndicator.SetActive(false);
        bearIndicator.SetActive(false);

    }

    public void ShowDogPanel()
    {
        if (dogPanel.activeSelf)
            return;
        dogPanel.SetActive(true);
        playerPanel.SetActive(false);

        playerIndicator.SetActive(false);
        dogIndicator.SetActive(true);
        bearIndicator.SetActive(false);

    }

    public void HideAllPanel()
    {
        dogPanel.SetActive(false);
        playerPanel.SetActive(false);

        playerIndicator.SetActive(false);
        dogIndicator.SetActive(false);
        bearIndicator.SetActive(true);
    }    
}
