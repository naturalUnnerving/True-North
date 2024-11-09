using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour
{
	// Initialize player positions list
	[SerializeField] private List<Vector3> dogPositions = new List<Vector3>();
	// Link dog navmesh angent
	[SerializeField] private NavMeshAgent dog;
	// Index for dog positions list
	[SerializeField] private int positionIndex;
	// Reference to player and bear gameobject
	public GameObject bear;
	public GameObject player;
	// Toggle attack mode (FOR TESING ONLY)
	//[SerializeField] private bool attackMode = false;
	
    // Start is called before the first frame update
    void Start()
    {
        // Set dog positions for attack mode, each of which are located on the cardinal directions of the bear,
		// located 15 units from the bear positon (we can change this later)
		Vector3 bearFront = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z - 15f);
		Vector3 bearBack = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z + 15f);
		Vector3 bearLeft = new Vector3(bear.transform.position.x + 15f, bear.transform.position.y, bear.transform.position.z);
		Vector3 bearRight = new Vector3(bear.transform.position.x - 15f, bear.transform.position.y, bear.transform.position.z);
		dogPositions.Add(bearFront); // pos 0
		dogPositions.Add(bearLeft); // pos 1
		dogPositions.Add(bearBack); // pos 2
		dogPositions.Add(bearRight); // pos 3
		
		// Initialize dog starting position to be next to player
		positionIndex = 0;
		//dog.destination = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /* TESTING, NOT FINAL*/
		
		// Face bear while stationary
		if (dog.velocity == Vector3.zero) faceBear();
		
        // Using left arrow for left and right arrow for right, 
		// up arrow for forward and down arrow for backward, 
		// move through the bear positions. try not to spam >:]
		if (Input.GetKeyDown("up"))
		{
			// Go to the bear
			dog.destination = bear.transform.position;
		}
		
		if (Input.GetKeyDown("down"))
		{
			// Return to original outer position
			dog.destination = dogPositions[positionIndex];
		}
		
		if (Input.GetKeyDown("left"))
		{
			// Cycle through position index and set the destination
			positionIndex--;
			if (positionIndex < 0) positionIndex = 3;
			dog.destination = dogPositions[positionIndex];
		}
		
		if (Input.GetKeyDown("right"))
		{
			// Cycle through position index and set the destination
			positionIndex++;
			if (positionIndex > 3) positionIndex = 0;
			dog.destination = dogPositions[positionIndex];
		}
    }
	
	// Gradually turn the dog towards the bear
	void faceBear()
	{
		Vector3 moveDirection = bear.transform.position - dog.transform.position;
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
		dog.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
	}
}
