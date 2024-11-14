using UnityEngine;
using TrueNorth.CharacterStats;

// Main Player Class
public class Player
{
	// Player stats (for now just for the sake of testing). refer to CharacterStat constructor
	public CharacterStat HP;
	public CharacterStat AP;
	public CharacterStat attack;
	public CharacterStat defense;
	public CharacterStat speed;
	
	// Constructor
	public Player()
	{
		HP = new CharacterStat(100f);
		AP = new CharacterStat(7f);
		attack = new CharacterStat(30f);
		defense = new CharacterStat(20f);
		speed = new CharacterStat(30f);
	}
	
	// Player fire
	// Damage: front not very effective (x0.5), side effective (x1.0), back super effective (x1.5)
	public void Fire()
	{
		// DEBUG CALL
		Debug.Log("PLAYER FIRE");
	}
	
	// Player reload (done while idle?)
	public void Reload()
	{
		// DEBUG CALL
		Debug.Log("PLAYER RELOAD");
	}
	
	// Player move, one relocation takes a turn. place code from playermovement.cs
	public void Move()
	{
		
	}
}
