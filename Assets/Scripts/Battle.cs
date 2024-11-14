using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main battle scene
public class Battle : MonoBehaviour
{
	// Battle system flags
	//[SerializeField] private bool playerIdle;
	//[SerializeField] private bool dogIdle;
	//[SerializeField] private bool bearIdle;
	
	// Player, dog and bear battle stamina
	[SerializeField] private float playerStaminaGauge;
	[SerializeField] private float dogStaminaGauge;
	[SerializeField] private float bearStaminaGauge;
	
	// Player, dog and bear initial health
	[SerializeField] private float playerHP;
	[SerializeField] private float dogHP;
	[SerializeField] private float bearHP;
	
	// Initialize actors
	Player player = new Player();
	Dog dog = new Dog();
	Bear bear = new Bear();
	
    // Start is called before the first frame update
    void Start()
    {
        /* Instanciate actors, play battle music (when music system is set up),
		play opening animations, dialogue, etc...
		Run when battle scene loads */
		
		// Initialize system flags
		//playerIdle = true;
		//dogIdle = true;
		//bearIdle = true;
		
		// Initialize stamina gauges
		playerStaminaGauge = 0f;
		dogStaminaGauge = 0f;
		bearStaminaGauge = 0f;
		
		// Initialize actor HP, drawn from each charachter class
		playerHP = player.HP.BaseValue;
		dogHP = dog.HP.BaseValue;
		bearHP = bear.HP.BaseValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for victory and gameOver
		//if (playerHP <= 0f && dogHP <= 0f) gameOver();
		//if (bearHP <= 0f) victory();
		
		// Increase actor stamina gauges
		if (playerStaminaGauge < 100f) playerStaminaGauge += player.stamina.BaseValue * Time.deltaTime;
		if (dogStaminaGauge < 100f) dogStaminaGauge += dog.stamina.BaseValue * Time.deltaTime;
		if (bearStaminaGauge < 100f) bearStaminaGauge += bear.stamina.BaseValue * Time.deltaTime;
		
		// Run action loops
		playerAction();
		dogAction();
		bearAction();
	}
	
	// Communicates with UI to execute a player action. for now use R, F to call fire and reload
	void playerAction()
	{
		if (Input.GetKeyDown("r"))
		{
			if (playerStaminaGauge >= 100f)
			{
				playerStaminaGauge = 0f;
				player.Fire();
			}
			else
			{
				Debug.Log("not enough stamina!");
			}
		}
		
		// Maybe allow reload while idle?
		if (Input.GetKeyDown("f"))
		{
			if (playerStaminaGauge >= 100f)
			{
				playerStaminaGauge = 0f;
				player.Reload();
			}
			else
			{
				Debug.Log("not enough stamina!");
			}
		}
		
		// TO DO: set up movement calls
	}
	
	// Communicates with UI to execute a dog action, for now use t, g to call guard and bite
	void dogAction()
	{
		if (Input.GetKeyDown("t"))
		{
			if (dogStaminaGauge >= 100f)
			{
				dogStaminaGauge = 0f;
				dog.Guard();
			}
			else
			{
				Debug.Log("not enough stamina!");
			}
		}
		
		if (Input.GetKeyDown("g"))
		{
			if (dogStaminaGauge >= 100f)
			{
				dogStaminaGauge = 0f;
				dog.Bite();
			}
			else
			{
				Debug.Log("not enough stamina!");
			}
		}
		
		// TO DO: set up movement calls
	}
	
	// bear actions are called by bear AI. For now just autorun Swipe
	void bearAction()
	{
		if (bearStaminaGauge >= 100f)
		{
			bearStaminaGauge = 0f;
			bear.Swipe();
		}
		
		// TO DO: set up bear combat AI to decide whether to turn, growl or attack
	}
	
	// Show game over screen and send back to title for now
	void gameOver()
	{
		// TO DO: set up game over screen
	}
	
	// Show victory screen and send back to title for now
	void victory()
	{
		// TO DO: set up victory screen
	}
}
