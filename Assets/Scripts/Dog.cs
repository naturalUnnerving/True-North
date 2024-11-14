using UnityEngine;
using TrueNorth.CharacterStats;

// Main dog class
public class Dog
{
	// Dog stats (for now just for the sake of testing)
	public CharacterStat HP;
	public CharacterStat AP;
	public CharacterStat attack;
	public CharacterStat defense;
	public CharacterStat speed;
	
	// Contructor
	public Dog()
	{
		HP = new CharacterStat(75f);
		AP = new CharacterStat(14f);
		attack = new CharacterStat(20f);
		defense = new CharacterStat(20f);
		speed = new CharacterStat(40f);
	}
	
    // Dog bark/guard (unable to pass)
	public void Guard()
	{
		// DEBUG CALL
		Debug.Log("DOG GUARD");
	}
	
	// Dog bite
	public void Bite()
	{
		// DEBUG CALL
		Debug.Log("DOG BITE");
	}
	
	// Dog move, one relocation takes a turn. place code from dogmovement.cs
	public void Move()
	{
		
	}
}
