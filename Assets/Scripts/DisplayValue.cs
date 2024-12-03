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
        UpdateText(battleScript.playerHP, battleScript.dogHP, battleScript.bearHP, battleScript.playerAPGauge, battleScript.dogAPGauge, battleScript.bearAPGauge);
    }
	
	void UpdateText (float value1, float value2, float value3, float value4, float value5, float value6) {
        //Update the text shown in the text component by setting the `text` variable
        textComponent.text = "Hunter HP: " + value1 + "\n" + "Akita HP: " + value2 + "\n" +
		"Bear HP: " + value3 + "\n" + "Hunter AP: " + value4 + "\n" + "Akita AP: " + value5 + "\n" +
		"Bear AP: " + value6 + "\n";
    }
}
