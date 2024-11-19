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
	
	// Player, dog and bear battle stamina
	public float playerAPGauge;
	public float dogAPGauge;
	public float bearAPGauge;
	
	// Player, dog and bear initial health
	public float playerHP;
	public float dogHP;
	public float bearHP;
	
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
		
		// Set player to alive and to have first turn
		currentHealthState = HealthState.alive;
		currentTurn = Turn.player;
		
		// Initialize stamina gauges
		playerAPGauge = playerScript.AP.BaseValue;
		dogAPGauge = dogScript.AP.BaseValue;
		bearAPGauge = bearScript.AP.BaseValue;
		
		// Initialize actor HP, drawn from each charachter class
		playerHP = playerScript.HP.BaseValue;
		dogHP = dogScript.HP.BaseValue;
		bearHP = bearScript.HP.BaseValue;
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
		
		// Set turns
		if (playerAPGauge <= 0f)
		{
			playerAPGauge = playerScript.AP.BaseValue;
			currentTurn = Turn.dog;
		}
		
		if (dogAPGauge <= 0f)
		{
			dogAPGauge = dogScript.AP.BaseValue;
			currentTurn = Turn.bear;
		}
		
		if (bearAPGauge <= 0f)
		{
			bearAPGauge = bearScript.AP.BaseValue;
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
		
		if (Input.GetKeyDown("a"))
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
		
		if (Input.GetKeyDown("d"))
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
		if (Input.GetKeyDown("t"))
		{
			if (dogAPGauge >= 3f)
			{
				dogAPGauge -= 3f;
				dogScript.Guard();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("g"))
		{
			if (dogAPGauge >= 5f && dogClose)
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
