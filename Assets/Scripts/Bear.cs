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
	
	// Battle scene link
	public GameObject battle;
	public Battle battleScript;
	public BearMovement bearMovementScript;
	
	// Animation
	private Animator anim;

	// raycast for bite
	Ray ray;
	RaycastHit hitData;
	public float dist;

	// Bear VFX
	public GameObject growlVFX;
	public GameObject growlAuraVFX;
	public GameObject swipeVFX;

	[SerializeField]
	private float growlVFXDuration;
	[SerializeField]
	private float growlVFXDelay;
	[SerializeField]
	private float growlAuraVFXDuration;
	[SerializeField]
	private float swipeVFXDelay;
	[SerializeField]
	private float swipeVFXDuration;
	
	void Start()
	{
		battleScript = battle.GetComponent<Battle>();
		anim = GetComponentInChildren<Animator>();
		bearMovementScript = battleScript.bear.GetComponent<BearMovement>();
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
			if(growlAuraVFX != null && growlVFX != null)
			{
				await Task.Delay(TimeSpan.FromSeconds(growlVFXDelay));
				growlVFX.SetActive(true);
				growlAuraVFX.SetActive(true);
				await Task.Delay(TimeSpan.FromSeconds(growlVFXDuration));
				await Task.Delay(TimeSpan.FromSeconds(growlAuraVFXDuration));
				growlVFX.SetActive(false);
				growlAuraVFX.SetActive(false);
			}
		}
		else
		{
			Debug.Log("Not enough AP!");
		}
	}
	
	// Bear swipe
	public async void Swipe()
	{
		
		if (battleScript.bearAPGauge >= 5f && battleScript != null)
		{
			// DEBUG CALL
			Debug.Log("BEAR SWIPE");
		
			// Play swipe animation and sound
			if (anim != null)
			{
				anim.Play("Base Layer.RIG-Armature|RIG-ANIM_Swipe", 0, 0f);
			}
			
			if (bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 8)
			{
				if (battleScript.playerMovementScript.far) battleScript.playerHP -= battleScript.bearAT;
				if (battleScript.playerMovementScript.middle) battleScript.playerHP -= battleScript.bearAT;
				if (battleScript.playerMovementScript.near) battleScript.playerHP -= battleScript.bearAT;
			}
			else if ((bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 8) && !battleScript.dogDead)
			{
				if (battleScript.dogMovementScript.far) battleScript.dogHP -= battleScript.bearAT;
				if (battleScript.dogMovementScript.middle) battleScript.dogHP -= battleScript.bearAT;
				if (battleScript.dogMovementScript.near) battleScript.dogHP -= battleScript.bearAT;
			}
			else
			{
				Debug.Log("SWIPE MISS");
			}
			
			battleScript.bearAPGauge -= 5f;

			// Play Swipe VFX
			if(swipeVFX != null)
			{
				await Task.Delay(TimeSpan.FromSeconds(swipeVFXDelay));
				swipeVFX.SetActive(true);
				await Task.Delay(TimeSpan.FromSeconds(swipeVFXDuration));
				swipeVFX.SetActive(false);
			}
			else {
				Debug.Log("Swipe VFX not found");
			}
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
			if (((bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 8) || ((bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 8) && !battleScript.dogDead)) && battleScript.bearAPGauge >= 5f)
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
			else if (!((bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 8) || ((bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 8) && !battleScript.dogDead)) && battleScript.bearAPGauge >= 3f)
			{
				battleScript.bearAPGauge -= 3f;
				if (battleScript.bearTurnDirection == 0) bearMovementScript.TurnLeft();
				if (battleScript.bearTurnDirection == 1) bearMovementScript.TurnRight();
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
			if (((bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 8) || ((bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 8) && !battleScript.dogDead)) && battleScript.bearAPGauge >= 5f)
			{
				Swipe();
				battleScript.endAction();
			}
			else if (!((bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.playerMovementScript.positionIndex - 8) || ((bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 4 || bearMovementScript.positionIndex == battleScript.dogMovementScript.positionIndex - 8) && !battleScript.dogDead)) && battleScript.bearAPGauge >= 3f)
			{
				battleScript.bearAPGauge -= 3f;
				if (battleScript.bearTurnDirection == 0) bearMovementScript.TurnLeft();
				if (battleScript.bearTurnDirection == 1) bearMovementScript.TurnRight();
				battleScript.endAction();
			}
			else
			{
				battleScript.bearAPGauge = 0f;
			}
		}
	}
}
