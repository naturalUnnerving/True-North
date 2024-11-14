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
	[SerializeField] private float playerAPGauge;
	[SerializeField] private float dogAPGauge;
	[SerializeField] private float bearAPGauge;
	
	// Player, dog and bear initial health
	[SerializeField] private float playerHP;
	[SerializeField] private float dogHP;
	[SerializeField] private float bearHP;
	
	// Initialize actors
	Player player = new Player();
	Dog dog = new Dog();
	Bear bear = new Bear();
	
	//Creates enumerators for the different game states
	public enum HealthState
    {
        alive = 0,
        playerDead = 1,
        bearDead = 2
    }
	
	public enum Turn
    {
        player = 0,
		dog = 1,
        bear = 2
    }
	
	// Helath state and turn variable
	HealthState currentHealthState = HealthState.alive;
	Turn currentTurn;
	
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
		
		// Set player to alive and to have first turn
		currentHealthState = HealthState.alive;
		currentTurn = Turn.player;
		
		// Initialize stamina gauges
		playerAPGauge = player.AP.BaseValue;
		dogAPGauge = dog.AP.BaseValue;
		bearAPGauge = bear.AP.BaseValue;
		
		// Initialize actor HP, drawn from each charachter class
		playerHP = player.HP.BaseValue;
		dogHP = dog.HP.BaseValue;
		bearHP = bear.HP.BaseValue;
    }

    // Update is called once per frame
    void Update()
    {
		//Checks Players current Health State
        if (playerHP != 0 && dogHP != 0)
        {
            currentHealthState = HealthState.alive;
        }
        if (playerHP == 0 || dogHP == 0)
        {
            currentHealthState = HealthState.playerDead;
        } 
        else if (bearHP == 0)
        {
            currentHealthState = HealthState.bearDead;
        }
		
        //Switches case of currentHealthState when the above cases trigger
        switch (currentHealthState)
        {
            case HealthState.alive:
                // alive
                break;
            case HealthState.playerDead:
                gameOver();
                break;
            case HealthState.bearDead:
                victory();
                break;
        }
		
		// Set turns
		if (playerAPGauge <= 0f)
		{
			playerAPGauge = player.AP.BaseValue;
			currentTurn = Turn.dog;
		}
		
		if (dogAPGauge <= 0f)
		{
			dogAPGauge = dog.AP.BaseValue;
			currentTurn = Turn.bear;
		}
		
		if (bearAPGauge <= 0f)
		{
			bearAPGauge = bear.AP.BaseValue;
			currentTurn = Turn.player;
		}
		
		// Switch case for turn
		switch (currentTurn)
		{
			case Turn.player: playerAction(); break;
			case Turn.dog: dogAction(); break;
			case Turn.bear: bearAction(); break;
		}
	}
	
	// Communicates with UI to execute a player action. for now use R, F to call fire and reload
	void playerAction()
	{
		if (Input.GetKeyDown("r"))
		{
			playerAPGauge -= 3f;
			player.Fire();
		}
		
		// Maybe allow reload while idle?
		if (Input.GetKeyDown("f"))
		{
			playerAPGauge -= 2f;
			player.Reload();
		}
		
		// TO DO: set up movement calls
	}
	
	// Communicates with UI to execute a dog action, for now use t, g to call guard and bite
	void dogAction()
	{
		if (Input.GetKeyDown("t"))
		{
			dogAPGauge -= 3f;
			dog.Guard();
		}
		
		if (Input.GetKeyDown("g"))
		{
			dogAPGauge -= 5f;
			dog.Bite();
		}
		
		// TO DO: set up movement calls
	}
	
	// bear actions are called by bear AI. For now just autorun Swipe
	void bearAction()
	{
		//bearAPGauge -= 0f;
		//bear.Swipe();
		
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
