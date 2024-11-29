using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Main battle scene
public class Battle : MonoBehaviour
{
	// Battle system flags
	public bool reload;
	public bool dogClose;
	public bool bark;
	public bool growl;
	
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
	public CharacterMovement bearMovementScript;
	
	//Scenes
	[SerializeField] private string victoryScreen;
	[SerializeField] private string gameOverScreen;

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
		bearMovementScript = bear.GetComponent<CharacterMovement>();
		
		// Initialize battle flags
		reload = false;
		dogClose = false;
		bark = false;
		growl = false;
		
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
		if (playerMovementScript.character.velocity == Vector3.zero) playerMovementScript.faceBear();
		if (dogMovementScript.character.velocity == Vector3.zero) dogMovementScript.faceBear();
		
		// Set turns and reset status
		if (playerAPGauge <= 0f)
		{
			playerAPGauge = playerScript.AP.Value;
			currentTurn = Turn.dog;
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
			if (playerAPGauge >= 3f && !reload)
			{
				playerAPGauge -= 3f;
				playerScript.Fire();
				reload = true;
			}
			else if (reload)
			{
				Debug.Log("Must reload!");
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("f"))
		{
			if (playerAPGauge >= 2f && reload)
			{
				playerAPGauge -= 2f;
				playerScript.Reload();
				reload = false;
			}
			else if (!reload)
			{
				Debug.Log("Already loaded!");
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("a") && !growl)
		{
			if (playerAPGauge >= 1f)
			{
				playerAPGauge -= 1f;
				playerMovementScript.MoveLeft();
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
			if (dogAPGauge >= 3f)
			{
				dogAPGauge -= 3f;
				dogScript.Bark();
				bark = true;
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("f"))
		{
			if (dogAPGauge >= 5f && dogClose && dogScript.dist <= 2f)
			{
				dogAPGauge -= 5f;
				dogScript.Bite();
			}
			else if (!dogClose)
			{
				Debug.Log("Dog not close enough");
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("left"))
		{
			if (dogAPGauge >= 1f)
			{
				dogAPGauge -= 1f;
				dogMovementScript.MoveLeft();
				dogClose = false;
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
	void bearAction()
	{
		// TO DO: implement bear AI
		
		// DEBUG ONLY
		// End turn
		if (Input.GetKeyDown("q")) bearAPGauge = 0f;
		
		if (Input.GetKeyDown("r"))
		{
			bearAPGauge -= 5f;
			bearScript.Growl();
			growl = true;
		}
		
		if (Input.GetKeyDown("f"))
		{
			bearAPGauge -= 5f;
			bearScript.Swipe();
		}
		
		if (Input.GetKeyDown("z"))
		{
			if (bearAPGauge >= 1f)
			{
				bearAPGauge -= 1f;
				bearMovementScript.TurnLeft();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("c"))
		{
			if (bearAPGauge >= 1f)
			{
				bearAPGauge -= 1f;
				bearMovementScript.TurnRight();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
	}
	
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
}
