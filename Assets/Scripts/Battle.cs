using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main battle scene
public class Battle
{
	// Battle system flags
	[SerializeField] private bool playerIdle;
	[SerializeField] private bool dogIdle;
	[SerializeField] private bool bearIdle;
	
	// Player, dog and bear battle stamina
	[SerializeField] private float playerStaminaGauge;
	[SerializeField] private float dogStaminaGauge;
	[SerializeField] private float bearStaminaGauge;
	
	// Player, dog and bear initial health
	[SerializeField] private float playerHP;
	[SerializeField] private float dogHP;
	[SerializeField] private float bearHP;
	
    // Start is called before the first frame update
    void Start()
    {
        /* Instanciate actors, play battle music (when music system is set up),
		play opening animations, dialogue, etc...
		Run when battle scene loads */
		
		// Initialize system flags
		playerIdle = true;
		dogIdle = true;
		bearIdle = true;
		
		// Initialize stamina gauges
		playerStaminaGauge = 0f;
		dogStaminaGauge = 0f;
		bearStaminaGauge = 0f;
		
		// Initialize actor HP, drawn from each charachter class
		//playerHP = player.maxHP;
		//dogHP = dog.maxHP;
		//bearHP = bear.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for victory and gameOver
		//if (playerHP <= 0f && dogHP <= 0f) gameOver();
		//if (bearHP <= 0f) victory();
		
		// Increase actor stamina gauges
		//increaseStamina(playerStaminaGauge, player.stamina);
		//increaseStamina(dogStaminaGauge, dog.stamina);
		//increaseStamina(bearStaminaGauge, bear.stamina);
		
		// Run action loops if someone is not idle
		if (!playerIdle || !dogIdle || !bearIdle)
		{
			playerAction();
			dogAction();
			bearAction();
		}
	}
	
	// Communicates with UI to execute a player action
	void playerAction()
	{
		
	}
	
	// Communicates with UI to execute a dog action
	void dogAction()
	{
		
	}
	
	// Communicates with UI to execute a bear action
	void bearAction()
	{
		
	}
	
	// Show game over screen and send back to title for now
	void gameOver()
	{
		
	}
	
	// Show victory screen and send back to title for now
	void victory()
	{
		
	}
	
	// Increase stamina gauge for actor, depending on actor's base stamina stat
	void increaseStamina(float baseStamina, float staminaRate)
	{
		while (baseStamina < 100f)
		{
			baseStamina += staminaRate * Time.deltaTime;
		}
	}
}
