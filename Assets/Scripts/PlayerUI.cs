using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Battle battle;
    [SerializeField] Player player;
    [SerializeField] Button moveRight;
    [SerializeField] Button moveLeft;
    [SerializeField] Button fire;
    [SerializeField] Button reload;
    [SerializeField] Button quit;
    [SerializeField] GameObject action;
    [SerializeField] TextMeshProUGUI actionText; 
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] GameObject playerAP;
    [SerializeField] Sprite disableAP;
    [SerializeField] Sprite enableAP;
    [SerializeField] TextMeshProUGUI apText;
    [SerializeField] float moveAP;
    [SerializeField] float fireAP;
    [SerializeField] float reloadAP;

    float playerHPMaxValue;
    float playerAPMaxValue;

    float lastAP;

    // Start is called before the first frame update
    void Start()
    {
        playerHPMaxValue = player.HP.Value;
        playerAPMaxValue = player.AP.Value;
        lastAP = playerAPMaxValue;

        moveLeft.onClick.AddListener(MoveLeftBtnPressed);
        moveRight.onClick.AddListener(MoveRightBtnPressed);
        fire.onClick.AddListener(FireBtnPressed);
        reload.onClick.AddListener(ReloadBtnPressed);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdatePlayerHealth();

        if (lastAP != battle.playerAPGauge)
        {
            UpdatePlayerAP();
        }

        if (battle.playerAPGauge < moveAP)
        {
            moveLeft.interactable = false;
            moveRight.interactable = false;
        }
        else
        {
            moveLeft.interactable = true;
            moveRight.interactable = true;
        }

        if (battle.playerAPGauge < fireAP || battle.reload)
        {
            fire.interactable = false;
        }
        else
        {
            fire.interactable = true;
        }

        if (battle.playerAPGauge < reloadAP )
        {
            reload.interactable = false;
        }
        else
            reload.interactable = true;
    }

    

    void UpdatePlayerHealth()
    {
        healthBar.fillAmount = battle.playerHP / playerHPMaxValue;
        healthText.text = battle.playerHP + "/" + playerHPMaxValue;
    }
    void UpdatePlayerAP()
    {
        if (lastAP > battle.playerAPGauge) 
        {
            while (lastAP != battle.playerAPGauge)
            {
                playerAP.transform.GetChild((int)lastAP - 1).GetComponent<Image>().sprite = disableAP;
                lastAP -= 1;
            }
        }
        else if (lastAP < battle.playerAPGauge)
        {
            while (lastAP != battle.playerAPGauge)
            {
                playerAP.transform.GetChild((int)lastAP).GetComponent<Image>().sprite = enableAP;
                lastAP += 1;
            }
        }
        
        apText.text = battle.playerAPGauge+ "/" + playerAPMaxValue;
    }


    void MoveLeftBtnPressed()
    {
        action.SetActive(true);
        actionText.text = "Move Left";
        StartCoroutine(HideActionAfter1Second());
    }

    void MoveRightBtnPressed()
    {
        action.SetActive(true);
        actionText.text = "Move Right";
        StartCoroutine(HideActionAfter1Second());
    }

    void FireBtnPressed()
    {
        action.SetActive(true);
        actionText.text = "Fire";
        StartCoroutine(HideActionAfter1Second());
    }

    void ReloadBtnPressed()
    {
        action.SetActive(true);
        actionText.text = "Reload";
        StartCoroutine(HideActionAfter1Second());
    }

    void QuitBtnPressed()
    {
        action.SetActive(true);
        actionText.text = "Quit";
        StartCoroutine(HideActionAfter1Second());
    }

    IEnumerator HideActionAfter1Second()
    {
        yield return new WaitForSeconds(3);
        action.SetActive(false);
    }
}
