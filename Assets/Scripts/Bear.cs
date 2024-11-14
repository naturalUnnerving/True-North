using UnityEngine;
using TrueNorth.CharacterStats;

// Main Bear class
public class Bear
{
	// Reference to gameobject for interactions
	[SerializeField] private UnityEngine.AI.NavMeshAgent bear;
	
	// Player stats (for now just for the sake of testing)
	public CharacterStat HP = new CharacterStat(500f);
	public CharacterStat stamina = new CharacterStat(20f);
	public CharacterStat attack = new CharacterStat(70f);
	public CharacterStat defense = new CharacterStat(100f);
	public CharacterStat speed = new CharacterStat(15f);
	
    // Bear Growl (intimidate, suggestion: lock movement)
	void Growl()
	{
		
	}
	
	// Bear swipe
	void Swipe()
	{
		
	}
	
	// Dog move, one relocation takes a turn. place code from bearmovement.cs
	void Turn()
	{
		
	}
}
