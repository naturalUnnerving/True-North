using UnityEngine;
using TrueNorth.CharacterStats;

// Main dog class
public class Dog : MonoBehaviour
{
	// Dog stats (for now just for the sake of testing)
	public CharacterStat HP = new CharacterStat(75f);
	public CharacterStat AP = new CharacterStat(14f);
	public CharacterStat attack = new CharacterStat(20f);
	public CharacterStat defense = new CharacterStat(20f);
	public CharacterStat speed = new CharacterStat(40f);
	
	// raycast for bite
	Ray ray;
	RaycastHit hitData;
	public float dist;
	
	// Battle scene link
	public GameObject battle;
	public Battle battleScript;
	
	// Animation
	private Animator anim;
	
	void Start()
	{
		battleScript = battle.GetComponent<Battle>();
		anim = GetComponentInChildren<Animator>();
	}
	
	void Update()
	{
		ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(ray, out hitData)) dist = hitData.distance;
	}
	
    // Dog bark/guard (reduce bear damage for next turn)
	public void Bark()
	{
		if (battleScript.dogAPGauge >= 3f)
		{
			battleScript.dogAPGauge -= 3f;
			// DEBUG CALL
			Debug.Log("DOG BARK");
		
			// Play bark animation and sound
			if (anim != null)
			{
				anim.Play("Base Layer.RIG-Armature|Bark", 0, 0f);
			}
		
			if (!battleScript.bark) battleScript.bearAT *= 0.5f;
			battleScript.bark = true;
		}
		else
		{
			Debug.Log("Not enough AP!");
		}
	}
	
	// Dog bite
	public void Bite()
	{
		if (battleScript.dogAPGauge >= 5f && battleScript.dogMovementScript.near)
			{
				battleScript.dogAPGauge -= 5f;
				// DEBUG CALL
				Debug.Log("DOG BITE");
		
				// Play bite animation and sound
				if (anim != null)
				{
					anim.Play("Base Layer.RIG-Armature|Bite", 0, 0f);
				}
		
				battleScript.bearHP -= battleScript.dogAT;
			}
			else if (!battleScript.dogMovementScript.near)
			{
				Debug.Log("Dog not close enough");
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
	}
}
