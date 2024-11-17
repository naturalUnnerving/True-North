using UnityEngine;
using TrueNorth.CharacterStats;

// Main Player Class
public class Player : MonoBehaviour
{
	// Player stats (for now just for the sake of testing). refer to CharacterStat constructor
	public CharacterStat HP = new CharacterStat(100f);
	public CharacterStat AP = new CharacterStat(7f);
	public CharacterStat attack = new CharacterStat(30f);
	public CharacterStat defense = new CharacterStat(20f);
	public CharacterStat speed = new CharacterStat(30f);
	
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
}
