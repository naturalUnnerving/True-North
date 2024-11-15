using UnityEngine;
using TrueNorth.CharacterStats;

// Main Bear class
public class Bear
{
	// Player stats (for now just for the sake of testing)
	public CharacterStat HP;
	public CharacterStat AP;
	public CharacterStat attack;
	public CharacterStat defense;
	public CharacterStat speed;
	
	// Constructor
	public Bear()
	{
		HP = new CharacterStat(500f);
		AP = new CharacterStat(10f);
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
	
	// Bear Turn, one relocation takes a turn. place code from bearmovement.cs
	public void TurnLeft()
	{
		
	}
	
	// Bear Turn, one relocation takes a turn. place code from bearmovement.cs
	public void TurnRight()
	{
		
	}
}
