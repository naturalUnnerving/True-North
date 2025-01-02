using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Main battle scene
public class Battle : MonoBehaviour
{
	// Battle system flags
	public bool wait;
	public float timer;
	public bool reload;
	public bool dogClose;
	public bool bark;
	public bool growl;
	public bool dogDead;
	
	// Random bear turn direction
	public int bearTurnDirection;
	
	// Player, dog and bear battle stamina
	public float playerAPGauge;
	public float dogAPGauge;
	public float bearAPGauge;
	
	// Player, dog and bear initial health
	public float playerHP;
	public float dogHP;
	public float bearHP;
	
	// Player, dog and bear initial attack
	public float playerAT;
	public float dogAT;
	public float bearAT;
	
	// Initialize actors
	public GameObject player;
	public GameObject dog;
	public GameObject bear;
	
	// Actor scripts
	public Player playerScript;
	public Dog dogScript;
	public Bear bearScript;
	public CharacterMovement playerMovementScript;
	public CharacterMovement dogMovementScript;
	//public BearMovement bearMovementScript;
	
	//Scenes
	[SerializeField] private string victoryScreen;
	[SerializeField] private string gameOverScreen;
	
	// Music system
	public AudioSource audioSource;

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
	HealthState currentHealthState;
	Turn currentTurn;
	
    // Start is called before the first frame update
    void Start()
    {
        /* Instanciate actors, play battle music (when music system is set up),
		play opening animations, dialogue, etc...
		Run when battle scene loads */
		playerScript = player.GetComponent<Player>();
		dogScript = dog.GetComponent<Dog>();
		bearScript = bear.GetComponent<Bear>();
		playerMovementScript = player.GetComponent<CharacterMovement>();
		dogMovementScript = dog.GetComponent<CharacterMovement>();
		//bearMovementScript = bear.GetComponent<BearMovement>();
		
		// Initialize battle flags
		wait = false;
		timer = 0f;
		reload = false;
		dogClose = false;
		bark = false;
		growl = false;
		dogDead = false;
		
		// Set player to alive and to have first turn
		currentHealthState = HealthState.alive;
		currentTurn = Turn.player;
		
		// Initialize stamina gauges
		playerAPGauge = playerScript.AP.Value;
		dogAPGauge = dogScript.AP.Value;
		bearAPGauge = bearScript.AP.Value;
		
		// Initialize actor HP, drawn from each charachter class
		playerHP = playerScript.HP.Value;
		dogHP = dogScript.HP.Value;
		bearHP = bearScript.HP.Value;
		
		// Player, dog and bear initial health
		playerAT = playerScript.attack.Value;
		dogAT = dogScript.attack.Value;
		bearAT = bearScript.attack.Value;
		
		// Play battle music
		audioSource.Play();
		
		// Set initial bear turn direction
		bearTurnDirection = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
		//Checks Players current Health State
        if (playerHP <= 0f)
        {
            currentHealthState = HealthState.playerDead;
        } 
        else if (bearHP <= 0f)
        {
            currentHealthState = HealthState.bearDead;
        }
		
		// Check if dog is alive
		if (dogHP <= 0f) dogDead = true;
		
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
		
		// Dog and player face bear while not moving
		if (playerMovementScript.character.remainingDistance <= .5f) playerMovementScript.faceBear();
		if (dogMovementScript.character.remainingDistance <= .5f) dogMovementScript.faceBear();
		
		// Set turns and reset status
		if (playerAPGauge <= 0f)
		{
			playerAPGauge = playerScript.AP.Value;
			if (dogDead)
			{
				currentTurn = Turn.bear;
			}
			else
			{
				currentTurn = Turn.dog;
			}
			growl = false;
		}
		
		if (dogAPGauge <= 0f)
		{
			dogAPGauge = dogScript.AP.Value;
			currentTurn = Turn.bear;
		}
		
		if (bearAPGauge <= 0f)
		{
			bearAPGauge = bearScript.AP.Value;
			bearAT = bearScript.attack.Value;
			bark = false;
			bearTurnDirection = Random.Range(0, 2);
			currentTurn = Turn.player;
		}
		
		if (!wait)
		{
			// Switch case for turn
			switch (currentTurn)
			{
				case Turn.player: playerAction(); break;
				case Turn.dog: dogAction(); break;
				case Turn.bear: bearScript.AI(); break;
			}
		}
		else
		{
			cooldown();
		}
	}
	
	// Communicates with UI to execute a player action. for now use R, F to call fire and reload
	void playerAction()
	{
		if (Input.GetKeyDown("r"))
		{
			playerScript.Fire();
			endAction();
		}
		
		if (Input.GetKeyDown("f"))
		{
			playerScript.Reload();
			endAction();
		}
		
		if (Input.GetKeyDown("a") && !growl)
		{
			if (playerAPGauge >= 1f)
			{
				playerAPGauge -= 1f;
				playerMovementScript.MoveLeft();
				endAction();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("d") && !growl)
		{
			if (playerAPGauge >= 1f)
			{
				playerAPGauge -= 1f;
				playerMovementScript.MoveRight();
				endAction();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		// End turn
		if (Input.GetKeyDown("q")) playerAPGauge = 0f;
	}
	
	// Communicates with UI to execute a dog action, for now use t, g to call guard and bite
	void dogAction()
	{
		if (Input.GetKeyDown("r"))
		{
			dogScript.Bark();
			endAction();
		}
		
		if (Input.GetKeyDown("f"))
		{
			dogScript.Bite();
			endAction();
		}
		
		if (Input.GetKeyDown("left"))
		{
			if (dogAPGauge >= 1f)
			{
				dogAPGauge -= 1f;
				dogMovementScript.MoveLeft();
				dogClose = false;
				endAction();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("right"))
		{
			if (dogAPGauge >= 1f)
			{
				dogAPGauge -= 1f;
				dogMovementScript.MoveRight();
				dogClose = false;
				endAction();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("up"))
		{
			if (dogAPGauge >= 1f && !dogClose)
			{
				dogAPGauge -= 1f;
				dogMovementScript.MoveUp();
				dogClose = true;
				endAction();
			}
			else if (dogClose)
			{
				Debug.Log("Dog already at bear!");
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("down"))
		{
			if (dogAPGauge >= 1f && dogClose)
			{
				dogAPGauge -= 1f;
				dogMovementScript.MoveDown();
				dogClose = false;
				endAction();
			}
			else if (!dogClose)
			{
				Debug.Log("Dog already away from bear!");
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		// End turn
		if (Input.GetKeyDown("q")) dogAPGauge = 0f;
	}
	
	// bear actions are called by bear AI. For now just autorun Swipe
	/*
	void bearAction()
	{
		// DEBUG ONLY
		// End turn
		if (Input.GetKeyDown("q")) bearAPGauge = 0f;
		
		if (Input.GetKeyDown("r"))
		{
			bearScript.Growl();
		}
		
		if (Input.GetKeyDown("f"))
		{
			bearScript.Swipe();
		}
		
		if (Input.GetKeyDown("z"))
		{
			if (bearAPGauge >= 3f)
			{
				bearAPGauge -= 3f;
				bearMovementScript.TurnLeft();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("c"))
		{
			if (bearAPGauge >= 3f)
			{
				bearAPGauge -= 3f;
				bearMovementScript.TurnRight();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
	}
	*/
	
	// Show game over screen and send back to title for now
	void gameOver()
	{
        SceneManager.LoadScene(gameOverScreen);
    }
	
	// Show victory screen and send back to title for now
	void victory()
	{
        SceneManager.LoadScene(victoryScreen);
    }
	
	// end anction and start cooldown
	public void endAction()
	{
		wait = true;
		timer = 0f;
	}
	
	// Wait until action cooldown ends
	void cooldown()
	{
		if (timer >= 1f)
		{
			wait = false;
		}
		else
		{
			timer += 0.25f * Time.deltaTime;
		}
	}
}
