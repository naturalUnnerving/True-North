using UnityEngine;
using TrueNorth.CharacterStats;

// Main dog class
public class Dog : MonoBehaviour
{
	// Dog stats (for now just for the sake of testing)
	public CharacterStat HP = new CharacterStat(75f);
	public CharacterStat AP = new CharacterStat(14f);
	public CharacterStat attack = new CharacterStat(20f);
	public CharacterStat defense = new CharacterStat(20f);
	public CharacterStat speed = new CharacterStat(40f);
	
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
}
