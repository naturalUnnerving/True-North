using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
	// Initialize player positions list
	[SerializeField] private List<Vector3> playerPositions = new List<Vector3>();
	// Link player navmesh angent
	[SerializeField] private NavMeshAgent player;
	// Index for player positions list
	[SerializeField] private int positionIndex;
	// Reference to bear gameobject
	public GameObject bear;
	
    // Start is called before the first frame update
    void Start()
    {
        // Set player positions, each of which are located on the cardinal directions of the bear,
		// located 15 units from the bear positon (we can change this later)
		Vector3 bearFront = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z - 15f);
		Vector3 bearBack = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z + 15f);
		Vector3 bearLeft = new Vector3(bear.transform.position.x + 15f, bear.transform.position.y, bear.transform.position.z);
		Vector3 bearRight = new Vector3(bear.transform.position.x - 15f, bear.transform.position.y, bear.transform.position.z);
		playerPositions.Add(bearFront); // pos 0
		playerPositions.Add(bearLeft); // pos 1
		playerPositions.Add(bearBack); // pos 2
		playerPositions.Add(bearRight); // pos 3
		
		// Initialize player starting position to front of bear
		positionIndex = 0;
		player.destination = playerPositions[positionIndex];
    }

    // Update is called once per frame
    void Update()
    {
		/* TESTING, NOT FINAL*/
		
		// Face bear while stationary
		if (player.velocity == Vector3.zero) faceBear();
		
        // Using A for left and D for right, move through the bear positions. try not to spam >:]
		if (Input.GetKeyDown("a"))
		{
			// Cycle through position index and set the destination
			positionIndex--;
			if (positionIndex < 0) positionIndex = 3;
			player.destination = playerPositions[positionIndex];
			faceTarget();
		}
		
		if (Input.GetKeyDown("d"))
		{
			// Cycle through position index and set the destination
			positionIndex++;
			if (positionIndex > 3) positionIndex = 0;
			player.destination = playerPositions[positionIndex];
			faceTarget();
		}
    }
	
	// Gradually turn the player towards the bear
	void faceBear()
	{
		Vector3 moveDirection = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z);
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
		player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
	}
	
	// Player faces direction they are moving in
	void faceTarget()
	{
		Vector3 moveDirection = new Vector3(player.velocity.x, player.velocity.y, player.velocity.z);
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
		player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
	}
}
