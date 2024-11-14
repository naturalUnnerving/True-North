using UnityEngine;
using TrueNorth.CharacterStats;

// Main Player Class
public class Player
{
	// Reference to gameobject for interactions
	[SerializeField] private UnityEngine.AI.NavMeshAgent player;
	
	// Player stats (for now just for the sake of testing). refer to CharacterStat constructor
	public CharacterStat HP = new CharacterStat(100f);
	public CharacterStat stamina = new CharacterStat(40f);
	public CharacterStat attack = new CharacterStat(30f);
	public CharacterStat defense = new CharacterStat(20f);
	public CharacterStat speed = new CharacterStat(30f);
	
	// Player fire
	// Damage: front not very effective (x0.5), side effective (x1.0), back super effective (x1.5)
	void Fire()
	{
		
	}
	
	// Player reload (done while idle?)
	void Reload()
	{
		
	}
	
	// Player move, one relocation takes a turn. place code from playermovement.cs
	void Move()
	{
		
	}
}
