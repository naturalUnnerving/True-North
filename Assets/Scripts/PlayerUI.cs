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
        else
        {
            while (lastAP != battle.playerAPGauge)
            {
                playerAP.transform.GetChild((int)lastAP - 1).GetComponent<Image>().sprite = enableAP;
                lastAP += 1;
            }
        }
        
        apText.text = battle.playerAPGauge+ "/" + playerAPMaxValue;
    }
    
}
