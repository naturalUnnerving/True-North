using UnityEngine;

public class PauseController : MonoBehaviour
{    
    public GameObject pauseMenu;
    [SerializeField] private ScreenFuntions screenFuntions;    
    public bool isPaused = false; 

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))

        {
            screenFuntions.PauseToggle(this);
        }

    }

}

