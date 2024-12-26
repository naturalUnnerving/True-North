using UnityEngine;
using TrueNorth.CharacterStats;
using System;
using System.Threading.Tasks;

// Main Bear class
public class Bear : MonoBehaviour
{
	// Player stats (for now just for the sake of testing)
	public CharacterStat HP = new CharacterStat(250f);
	public CharacterStat AP = new CharacterStat(15f);
	public CharacterStat attack = new CharacterStat(70f);
	public CharacterStat defense = new CharacterStat(100f);
	public CharacterStat speed = new CharacterStat(15f);
	
	// raycast for bite
	Ray ray;
	RaycastHit hitData;
	public float dist;
	
	// Battle scene link
	public GameObject battle;
	public Battle battleScript;
	
	// Animation
	private Animator anim;

	// Bear Growl VFX
	public GameObject growlVFX;

	[SerializeField]
	private float growlVFXDuration;
	
	void Start()
	{
		battleScript = battle.GetComponent<Battle>();
		anim = GetComponentInChildren<Animator>();
	}
	
	void Update()
	{
		ray = new Ray(transform.position, transform.forward);
	}
	
    // Bear Growl (intimidate, suggestion: lock movement)
	public async void Growl()
	{
		if (battleScript.bearAPGauge >= 5f)
		{
			// DEBUG CALL
			Debug.Log("BEAR GROWL");
		
			// Play growl animation and sound
			if (anim != null)
			{
				anim.Play("Base Layer.RIG-Armature|RIG-ANIM_Roar", 0, 0f);
			}
			
			battleScript.bearAPGauge -= 5f;
			battleScript.growl = true;

			// Play Growl VFX
			growlVFX.SetActive(true);
			await Task.Delay(TimeSpan.FromSeconds(growlVFXDuration));
			growlVFX.SetActive(false);
		}
		else
		{
			Debug.Log("Not enough AP!");
		}
	}
	
	// Bear swipe
	public void Swipe()
	{
		
		if (battleScript.bearAPGauge >= 5f)
		{
			// DEBUG CALL
			Debug.Log("BEAR SWIPE");
		
			// Play swipe animation and sound
			if (anim != null)
			{
				anim.Play("Base Layer.RIG-Armature|RIG-ANIM_Swipe", 0, 0f);
			}
		
			if (Physics.Raycast(ray, out hitData))
			{
				if (hitData.collider.gameObject.name == "Player")
				{
					battleScript.playerHP -= battleScript.bearAT;
				}
				else if (hitData.collider.gameObject.name == "Dog")
				{
					battleScript.dogHP -= battleScript.bearAT;
				}
			}
			else
			{
				Debug.Log("SWIPE MISS");
			}
			
			battleScript.bearAPGauge -= 5f;
		}
		else
		{
			Debug.Log("Not enough AP!");
		}
	}
}
