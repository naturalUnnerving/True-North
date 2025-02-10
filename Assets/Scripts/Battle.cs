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
	public bool bark;
	public bool growl;
	public bool dogDead;
	
	// Pause the game
	bool paused = false;

	//options screen is on
	bool optionsOn = false;
	
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
	public GameObject options;
	public GameObject pause;

	
	
	// Actor scripts
	public Player playerScript;
	public Dog dogScript;
	public Bear bearScript;
	public CharacterMovement playerMovementScript;
	public CharacterMovement dogMovementScript;
	public BearMovement bearMovementScript;
	
	//Scenes
	[SerializeField] private string victoryScreen;
	[SerializeField] private string gameOverScreen;
	[SerializeField] private string TitleScreen;
	[SerializeField] private string BattleScene;

	
	
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
		Time.timeScale = 1f;
        /* Instanciate actors, play battle music (when music system is set up),
		play opening animations, dialogue, etc...
		Run when battle scene loads */
		playerScript = player.GetComponent<Player>();
		dogScript = dog.GetComponent<Dog>();
		bearScript = bear.GetComponent<Bear>();
		playerMovementScript = player.GetComponent<CharacterMovement>();
		dogMovementScript = dog.GetComponent<CharacterMovement>();
		bearMovementScript = bear.GetComponent<BearMovement>();
		
		// Initialize battle flags
		wait = false;
		timer = 0f;
		reload = false;
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
		// Pause the game
		if(Input.GetKeyDown(KeyCode.Escape)) {
			paused = togglePause();
			PauseGame();
		}
		
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
		
		if (!paused)
		{
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
		
		if (Input.GetKeyDown("w") && !growl)
		{
			if (playerAPGauge >= 1f && !playerMovementScript.near)
			{
				playerAPGauge -= 1f;
				playerMovementScript.MoveUp();
				endAction();
			}
			else if (playerMovementScript.near)
			{
				playerMovementScript.MoveUp();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("s") && !growl)
		{
			if (playerAPGauge >= 1f && !playerMovementScript.far)
			{
				playerAPGauge -= 1f;
				playerMovementScript.MoveDown();
				endAction();
			}
			else if (playerMovementScript.far)
			{
				playerMovementScript.MoveDown();
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
				endAction();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("up"))
		{
			if (dogAPGauge >= 1f && !dogMovementScript.near)
			{
				dogAPGauge -= 1f;
				dogMovementScript.MoveUp();
				endAction();
			}
			else if (dogMovementScript.near)
			{
				dogMovementScript.MoveUp();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		if (Input.GetKeyDown("down"))
		{
			if (dogAPGauge >= 1f && !dogMovementScript.far)
			{
				dogAPGauge -= 1f;
				dogMovementScript.MoveDown();
				endAction();
			}
			else if (dogMovementScript.far)
			{
				dogMovementScript.MoveDown();
			}
			else
			{
				Debug.Log("Not enough AP!");
			}
		}
		
		// End turn
		if (Input.GetKeyDown("q")) dogAPGauge = 0f;
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
	
	// end anction and start cooldown
	public void endAction()
	{
		wait = true;
		timer = 0f;
	}
	
	// Wait until action cooldown ends
	void cooldown()
	{
		if (currentTurn == Turn.player)
		{
			if (timer >= 0.8f)
			{
				wait = false;
			}
			else
			{
				timer += 0.25f * Time.deltaTime;
			}
		}
		else if (currentTurn == Turn.dog)
		{
			if (timer >= 0.8f)
			{
				wait = false;
			}
			else
			{
				timer += 0.25f * Time.deltaTime;
			}
		}
		else if (currentTurn == Turn.bear)
		{
			if (timer >= 0.6f)
			{
				wait = false;
			}
			else
			{
				timer += 0.25f * Time.deltaTime;
			}
		}
	}
	
	// Pause the game
	void PauseGame()
	{
		//if the game is paused and the options screen is not showing, display this GUI Layout
		// if(paused && !optionsOn)
		// {
		// 	GUILayout.Label("Paused");
		// 	if(GUILayout.Button("Resume")) 
		// 	{
		// 		paused = togglePause();
		// 	}
		// 	if(GUILayout.Button("Options")) 
		// 	{

		// 		//New Method that shows the Options Prefab
		// 		options.SetActive(true);
		// 		optionsOn = true;

		// 	}
		// 	if(GUILayout.Button("Return to title"))
		// 	{
		// 		SceneManager.UnloadSceneAsync(BattleScene);
		// 		SceneManager.LoadScene(TitleScreen);
		// 	}
		// }

		if(paused && !optionsOn)
		{
			pause.SetActive(true);
		}
		else if(!paused && !optionsOn)
		{
			pause.SetActive(false);
		}
	}

	public void ResumeGame()
	{
		paused = togglePause();
		pause.SetActive(false);
	}

	public void ReturnToTitle()
	{
		SceneManager.UnloadSceneAsync(BattleScene);
		SceneManager.LoadScene(TitleScreen);
	}

	public void DisablePauseScreen(){
		pause.SetActive(false);
		paused = togglePause();
	}

	//this function is used in the back button inside of the PauseUI gameObject
	public void HandleOptionsScreen()
	{
		if(optionsOn)
		{
			options.SetActive(false);
			optionsOn = false;
		}
		else
		{
			options.SetActive(true);
			optionsOn = true;
		}
	}
	
	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			AudioListener.pause = false;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			AudioListener.pause = true;
			return(true);	
		}
	}
	
	public void PlayerFire()
	{
        playerScript.Fire();
    }

	public void PlayerReload()
	{
        playerScript.Reload();
    }

	public void PlayerMoveLeft()
	{
		if (!growl)
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
	}

	public void PlayerMoveRight()
	{
		if (!growl)
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
    }

	public void PlayerQuit()
	{
        playerAPGauge = 0f;
    }

	public void DogBark()
	{
        dogScript.Bark();
    }

	public void DogBit()
	{
        dogScript.Bite();
    }

	public void DogMoveLeft()
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

	public void DogMoveRight()
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

	public void DogMoveUp()
	{
        if (dogAPGauge >= 1f)
        {
            dogAPGauge -= 1f;
            dogMovementScript.MoveUp();
        }
        else
        {
            Debug.Log("Not enough AP!");
        }
    }

	public void DogMoveDown()
	{
        if (dogAPGauge >= 1f)
        {
            dogAPGauge -= 1f;
            dogMovementScript.MoveDown();
        }
        else
        {
            Debug.Log("Not enough AP!");
        }
    }

	public void DogQuit()
	{
        dogAPGauge = 0f;
    }
}
