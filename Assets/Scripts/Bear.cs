using UnityEngine;
using TrueNorth.CharacterStats;

// Main Bear class
public class Bear
{
	// Player stats (for now just for the sake of testing)
	public CharacterStat HP;
	public CharacterStat stamina;
	public CharacterStat attack;
	public CharacterStat defense;
	public CharacterStat speed;
	
	// Constructor
	public Bear()
	{
		HP = new CharacterStat(500f);
		stamina = new CharacterStat(20f);
		attack = new CharacterStat(70f);
		defense = new CharacterStat(100f);
		speed = new CharacterStat(15f);
	}
	
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
	
	// Dog move, one relocation takes a turn. place code from bearmovement.cs
	public void Turn()
	{
		
	}
}
