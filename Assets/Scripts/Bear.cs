using UnityEngine;
using TrueNorth.CharacterStats;

// Main Bear class
public class Bear : MonoBehaviour
{
	// Player stats (for now just for the sake of testing)
	public CharacterStat HP = new CharacterStat(500f);
	public CharacterStat AP = new CharacterStat(10f);
	public CharacterStat attack = new CharacterStat(70f);
	public CharacterStat defense = new CharacterStat(100f);
	public CharacterStat speed = new CharacterStat(15f);
	
	// raycast for bite
	Ray ray;
	RaycastHit hitData;
	public float dist;
	
	// Battle scene link
	public GameObject battle;
	public Battle battleScript;
	
	void Start()
	{
		battleScript = battle.GetComponent<Battle>();
	}
	
	void Update()
	{
		ray = new Ray(transform.position, transform.forward);
	}
	
    // Bear Growl (intimidate, suggestion: lock movement)
	public void Growl()
	{
		// DEBUG CALL
		Debug.Log("BEAR GROWL");
		
		// Play growl animation and sound
	}
	
	// Bear swipe
	public void Swipe()
	{
		// DEBUG CALL
		Debug.Log("BEAR SWIPE");
		
		// Play swipe animation and sound
		
		if (Physics.Raycast(ray, out hitData))
		{
			if (hitData.collider.gameObject.name == "Player")
			{
				battleScript.playerHP -= battleScript.bearAT;
			}
			else if (hitData.collider.gameObject.name == "Dog")
			{
				battleScript.dogHP -= battleScript.bearAT;
			}
		}
		else
		{
			Debug.Log("SWIPE MISS");
		}
	}
}
