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
	
    // Bear Growl (intimidate, suggestion: lock movement)
	public void Growl()
	{
		
	}
	
	// Bear swipe
	public void Swipe()
	{
		// DEBUG CALL
		Debug.Log("BEAR SWIPE");
	}
}
