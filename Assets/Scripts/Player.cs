using UnityEngine;
using TrueNorth.CharacterStats;
using System.Threading.Tasks;
using System;

// Main Player Class
public class Player : MonoBehaviour
{
	// Player stats (for now just for the sake of testing). refer to CharacterStat constructor
	public CharacterStat HP = new CharacterStat(100f);
	public CharacterStat AP = new CharacterStat(7f);
	public CharacterStat attack = new CharacterStat(30f);
	public CharacterStat defense = new CharacterStat(20f);
	public CharacterStat speed = new CharacterStat(30f);
	
	// Animation
	private Animator anim;
	
	// Sound Effects
	//public AudioSource audioSource;
	
	// raycast for rifle
	Ray ray;
	RaycastHit hitData;
	float angle;
	
	// Battle scene link
	public GameObject battle;
	public Battle battleScript;

	// Rifle VFX
	public GameObject rifleVFX;
	
	[SerializeField]
	private float rifleVFXDuration;
	[SerializeField]
	private float rifleVFXDelay;
	
	void Start()
	{
		battleScript = battle.GetComponent<Battle>();
		anim = GetComponentInChildren<Animator>();
	}
	
	void Update()
	{
		ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(ray, out hitData)) angle = Vector3.Angle(hitData.transform.forward, ray.direction);
	}
	
	// Player fire
	// Damage: front not very effective (x0.5), side effective (x1.0), back super effective (x1.5)
	public async void Fire()
	{
		if (battleScript.playerAPGauge >= 3f && !battleScript.reload)
		{
			battleScript.playerAPGauge -= 3f;
			// DEBUG CALL
			Debug.Log("PLAYER FIRE");

			battleScript.reload = true;
		
			// Play shooting animation and sound
			if (anim != null)
			{
				//anim.Play("Base Layer.Murata22Armtr|Firing", 0, 0f);
				anim.Play("Base Layer.RIG-matagiHunter_arm|Firing", 0, 0f);
			}
		
			//audioSource.PlayOneShot(audioSource.clip, 1.0f);

			// Play rifle VFX
			await Task.Delay(TimeSpan.FromSeconds(rifleVFXDelay));
			rifleVFX.SetActive(true);
			await Task.Delay(TimeSpan.FromSeconds(rifleVFXDuration));
			rifleVFX.SetActive(false);
		
			if (Physics.Raycast(ray, out hitData))
			{
				// Play bear hit animation and sound
				if ((angle >= -45f && angle <= 0f) || (angle <= 45f && angle > 0f))
				{
					battleScript.bearHP -= 0.5f * battleScript.playerAT; // front shot
				}
				else if ((angle >= -135f && angle <= -45f) || (angle >= 45f && angle <= 135f))
				{
					battleScript.bearHP -= 1.5f * battleScript.playerAT; // side shot
				}
				else if (angle <= -135f || angle >= 135f)
				{
					battleScript.bearHP -= 1.0f * battleScript.playerAT; // back shot
				}
			}
		}
		else if (battleScript.reload)
		{
			Debug.Log("Must reload!");
		}
		else
		{
			Debug.Log("Not enough AP!");
		}
	}
	
	// Player reload (done while idle?)
	public void Reload()
	{
		if (battleScript.playerAPGauge >= 2f && battleScript.reload)
		{
			battleScript.playerAPGauge -= 2f;
			// DEBUG CALL
			Debug.Log("PLAYER RELOAD");
		
			// Play player reload animation and sound
			if (anim != null)
			{
				//anim.Play("Base Layer.Murata22Armtr|Reloading", 0, 0f);
				anim.Play("Base Layer.RIG-matagiHunter_arm|Reloading");
			}
			battleScript.reload = false;
		}
		else if (!battleScript.reload)
		{
			Debug.Log("Already loaded!");
		}
		else
		{
			Debug.Log("Not enough AP!");
		}
	}
}
