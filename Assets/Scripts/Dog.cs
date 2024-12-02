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
		// DEBUG CALL
		Debug.Log("DOG BARK");
		
		// Play bark animation and sound
		if (anim != null)
        {
            anim.Play("Base Layer.RIG-Armature|Bark", 0, 0f);
        }
		
		if (!battleScript.bark) battleScript.bearAT *= 0.5f;
	}
	
	// Dog bite
	public void Bite()
	{
		// DEBUG CALL
		Debug.Log("DOG BITE");
		
		// Play bite animation and sound
		if (anim != null)
        {
            anim.Play("Base Layer.RIG-Armature|Bite", 0, 0f);
        }
		
		battleScript.bearHP -= battleScript.dogAT;
	}
}
