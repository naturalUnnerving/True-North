using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DogUI : MonoBehaviour
{
    [SerializeField] Battle battle;
    [SerializeField] Dog dog;
    [SerializeField] Button moveLeft;
    [SerializeField] Button moveRight;
    [SerializeField] Button moveUp;
    [SerializeField] Button moveDown;
    [SerializeField] Button bite;
    [SerializeField] Button bark;
    [SerializeField] Button quit;
    [SerializeField] GameObject action;
    [SerializeField] TextMeshProUGUI actionText;
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] GameObject dogAP;
    [SerializeField] Sprite disableAP;
    [SerializeField] Sprite enableAP;
    [SerializeField] TextMeshProUGUI apText;
    [SerializeField] float moveAP;
    [SerializeField] float bitAP;
    [SerializeField] float barkAP;


    float dogHPMaxValue;
    float dogAPMaxValue;

    float lastAP;
    // Start is called before the first frame update
    void Start()
    {
        dogHPMaxValue = dog.HP.Value;
        dogAPMaxValue = dog.AP.Value;
        lastAP = dogAPMaxValue;

        moveLeft.onClick.AddListener(MoveLeftBtnPressed);
        moveRight.onClick.AddListener(MoveRightBtnPressed);
        moveUp.onClick.AddListener(MoveUpBtnPressed);
        moveDown.onClick.AddListener(MoveDownBtnPressed);
        bite.onClick.AddListener(BiteBtnPressed);
        bark.onClick.AddListener(BarkBtnPressed);
        quit.onClick.AddListener(QuitBtnPressed);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDogHealth();

        if (lastAP != battle.dogAPGauge)
        {
            UpdateDogAP();
        }

        if (battle.dogAPGauge < moveAP)
        {
            moveDown.interactable = false;
            moveUp.interactable = false;
            moveLeft.interactable = false;
            moveRight.interactable = false;
        }
        else
        {
            moveDown.interactable = true;
            moveUp.interactable = true;
            moveLeft.interactable = true;
            moveRight.interactable = true;
        }

        if (battle.dogAPGauge < bitAP ||!battle.dogMovementScript.near || dog.dist > 2f)
        {
            bite.interactable = false;
        }
        else
        {
            bite.interactable = true;
        }

        if (battle.dogAPGauge < barkAP)
        {
            bark.interactable = false;
        }
        else
        {
            bark.interactable = true;
        }

    }

    void UpdateDogHealth()
    {
        healthBar.fillAmount = battle.dogHP / dogHPMaxValue;
        healthText.text = battle.dogHP + "/" + dogHPMaxValue;
    }
    void UpdateDogAP()
    {
        if (lastAP > battle.dogAPGauge)
        {
            while (lastAP != battle.dogAPGauge)
            {
                dogAP.transform.GetChild((int)lastAP - 1).GetComponent<Image>().sprite = disableAP;
                lastAP -= 1;
            }
        }
        else
        {
            while (lastAP != battle.dogAPGauge)
            {
                dogAP.transform.GetChild((int)lastAP).GetComponent<Image>().sprite = enableAP;
                lastAP += 1;
            }
        }

        apText.text = battle.dogAPGauge + "/" + dogAPMaxValue;
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

    void MoveUpBtnPressed()
    {
        action.SetActive(true);
        actionText.text = "Move UP";
        StartCoroutine(HideActionAfter1Second());
    }

    void MoveDownBtnPressed()
    {
        action.SetActive(true);
        actionText.text = "Move Down";
        StartCoroutine(HideActionAfter1Second());
    }

    void BiteBtnPressed()
    {
        action.SetActive(true);
        actionText.text = "Bite";
        StartCoroutine(HideActionAfter1Second());
    }

    void BarkBtnPressed()
    {
        action.SetActive(true);
        actionText.text = "Bark";
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
