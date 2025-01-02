using UnityEngine.UI;
using UnityEngine;

public class DisplayValue : MonoBehaviour
{
	public Text textComponent;
	
	// Battle scene link
	public GameObject battle;
	public Battle battleScript;
	
    // Start is called before the first frame update
    void Start()
    {
        battleScript = battle.GetComponent<Battle>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText(battleScript.playerHP, battleScript.dogHP, battleScript.bearHP, battleScript.playerAPGauge, battleScript.dogAPGauge, battleScript.bearAPGauge, battleScript.timer, battleScript.playerMovementScript.positionIndex, battleScript.dogMovementScript.positionIndex, battleScript.bearScript.bearMovementScript.positionIndex, battleScript.bearTurnDirection);
    }
	
	void UpdateText (float value1, float value2, float value3, float value4, float value5, float value6, float value7, int value8, int value9, int value10, int value11) {
        //Update the text shown in the text component by setting the `text` variable
        textComponent.text = "Hunter HP: " + value1 + "\n" + "Akita HP: " + value2 + "\n" +
		"Bear HP: " + value3 + "\n" + "Hunter AP: " + value4 + "\n" + "Akita AP: " + value5 + "\n" +
		"Bear AP: " + value6 + "\n" + value7 + "\n" + "hunter pos: " + value8 + "\n" + "dog pos: "+ value9 + "\n" + "bear pos: " + value10 + "\n" + value11 + "\n";
    }
}
