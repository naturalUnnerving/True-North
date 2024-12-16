using UnityEngine;
using TrueNorth.CharacterStats;

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
	public CharacterMovement bearMovementScript;
	
	// Animation
	private Animator anim;
	
	void Start()
	{
		battleScript = battle.GetComponent<Battle>();
		anim = GetComponentInChildren<Animator>();
		bearMovementScript = battleScript.bear.GetComponent<CharacterMovement>();
	}
	
	void Update()
	{
		ray = new Ray(transform.position, new Vector3(0, 0, -transform.forward.z));
	}
	
    // Bear Growl (intimidate, suggestion: lock movement)
	public void Growl()
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
	
	public void AI()
	{
		if (battleScript.bearHP >= 100f)
		{
			// Normal routine
			if (Physics.Raycast(ray, out hitData) && battleScript.bearAPGauge >= 5f)
			{
				if (battleScript.bearAPGauge >= 12f)
				{
					Growl();
					battleScript.endAction();
				}
				else
				{
					Swipe();
					battleScript.endAction();
				}
			}
			else if (battleScript.bearAPGauge >= 3f)
			{
				battleScript.bearAPGauge -= 3f;
				switch (battleScript.bearTurnDirection)
				{
					case 0: bearMovementScript.TurnLeft(); break;
					case 1: bearMovementScript.TurnRight(); break;
				}
				battleScript.endAction();
			}
			else
			{
				battleScript.bearAPGauge = 0f;
			}
		}
		else
		{
			// desperate routine
			if (Physics.Raycast(ray, out hitData) && battleScript.bearAPGauge >= 5f)
			{
				Swipe();
				battleScript.endAction();
			}
			else if (battleScript.bearAPGauge >= 3f)
			{
				battleScript.bearAPGauge -= 3f;
				switch (battleScript.bearTurnDirection)
				{
					case 0: bearMovementScript.TurnLeft(); break;
					case 1: bearMovementScript.TurnRight(); break;
				}
				battleScript.endAction();
			}
			else
			{
				battleScript.bearAPGauge = 0f;
			}
		}
	}
}
