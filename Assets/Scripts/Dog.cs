using UnityEngine;
using TrueNorth.CharacterStats;

// Main dog class
public class Dog
{
	// Reference to gameobject for interactions
	[SerializeField] private UnityEngine.AI.NavMeshAgent dog;
	
	// Dog stats (for now just for the sake of testing)
	public CharacterStat HP = new CharacterStat(75f);
	public CharacterStat stamina = new CharacterStat(60f);
	public CharacterStat attack = new CharacterStat(20f);
	public CharacterStat defense = new CharacterStat(20f);
	public CharacterStat speed = new CharacterStat(40f);
	
    // Dog bark/guard (unable to pass)
	void Guard()
	{
		
	}
	
	// Dog bite
	void Bite()
	{
		
	}
	
	// Dog move, one relocation takes a turn. place code from dogmovement.cs
	void Move()
	{
		
	}
}
