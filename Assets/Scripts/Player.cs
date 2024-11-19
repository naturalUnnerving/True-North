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
	
	// raycast for rifle
	Ray ray;
	RaycastHit hitData;
	float angle;
	
	// Battle scene link
	public GameObject battle;
	public Battle battleScript;
	
	void Start()
	{
		battleScript = battle.GetComponent<Battle>();
	}
	
	void Update()
	{
		ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(ray, out hitData)) angle = Vector3.Angle(hitData.transform.forward, ray.direction);
	}
	
	// Player fire
	// Damage: front not very effective (x0.5), side effective (x1.0), back super effective (x1.5)
	public void Fire()
	{
		// DEBUG CALL
		Debug.Log("PLAYER FIRE");
		
		// Play shooting animation and sound
		
		if (Physics.Raycast(ray, out hitData))
		{
			// Play bear hit animation and sound
			if ((angle >= -45f && angle <= 0f) || (angle <= 45f && angle > 0f))
			{
				battleScript.bearHP -= 0.5f * attack.BaseValue; // front shot
			}
			else if ((angle >= -135f && angle <= -45f) || (angle >= 45f && angle <= 135f))
			{
				battleScript.bearHP -= 1.5f * attack.BaseValue; // side shot
			}
			else if (angle <= -135f || angle >= 135f)
			{
				battleScript.bearHP -= 1.0f * attack.BaseValue; // back shot
			}
		}
	}
	
	// Player reload (done while idle?)
	public void Reload()
	{
		// DEBUG CALL
		Debug.Log("PLAYER RELOAD");
		
		// Play player reload animation and sound
	}
}
