using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldBearScript : MonoBehaviour
{
    private SphereCollider sphere;
    [SerializeField] private GameObject worldCanvas;

    [SerializeField] private string battleSceneName;

    private bool isPlayerClose;

    private OverworldPlayerInputs playerInputs;

    void Awake()
    {
        playerInputs = FindAnyObjectByType<OverworldPlayerInputs>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sphere = GetComponent<SphereCollider>();
        worldCanvas.SetActive(false);
        playerInputs.OnInteractPressed += PlayerInputs_OnInteractPressed;
    }

    private void PlayerInputs_OnInteractPressed(object sender, System.EventArgs e)
    {
        if(isPlayerClose)
        {
            Debug.Log("Can Start Battle");
            SceneManager.LoadScene(battleSceneName);
        }
        else
        {
            Debug.Log("Cannot Start Battle");
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start Battle with E.");
        isPlayerClose = true;
        worldCanvas.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        isPlayerClose = false;
        worldCanvas.SetActive(false);
    }



}
