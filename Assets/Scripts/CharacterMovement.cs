using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
	// Initialize player positions list
	[SerializeField] private List<Vector3> characterPositions = new List<Vector3>();
	// Link dog navmesh angent
	public NavMeshAgent character;
	// Index for dog positions list
	public int positionIndex;
	// Reference to player and bear gameobject
	public GameObject bear;

	[SerializeField] float rotateTime = .75f;
	float targetAngel;
	float currentVelocity;
	float CurrentAngle;
	
    // Start is called before the first frame update
    void Start()
    {
        // Set dog positions for attack mode, each of which are located on the cardinal directions of the bear,
		// located 15 units from the bear positon (we can change this later)
		Vector3 bearFront = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z - 15f);
		Vector3 bearBack = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z + 15f);
		Vector3 bearLeft = new Vector3(bear.transform.position.x + 15f, bear.transform.position.y, bear.transform.position.z);
		Vector3 bearRight = new Vector3(bear.transform.position.x - 15f, bear.transform.position.y, bear.transform.position.z);
		characterPositions.Add(bearFront); // pos 0
		characterPositions.Add(bearLeft); // pos 1
		characterPositions.Add(bearBack); // pos 2
		characterPositions.Add(bearRight); // pos 3
		
		// Initialize dog starting position to be next to player
		positionIndex = 0;

		CurrentAngle = character.transform.eulerAngles.y;
		faceBear();
    }

    // Update is called once per frame
    void Update()
    {
		// Face bear while stationary
		//if (character.velocity == Vector3.zero) faceBear();

		CurrentAngle = Mathf.SmoothDampAngle(CurrentAngle, targetAngel, ref currentVelocity, rotateTime);
		character.transform.rotation = Quaternion.AngleAxis(CurrentAngle, Vector3.up);
    }
	
	// Dog only
	public void MoveUp()
	{
		// Go to the bear
		character.destination = bear.transform.position;
		faceBear();
    }
	
	// Dog only
	public void MoveDown()
	{
		// Return to original outer position
		character.destination = characterPositions[positionIndex];
		faceTarget();

    }
	
	// Dog and player only
	public void MoveLeft()
	{
		// Cycle through position index and set the destination
		positionIndex--;
		if (positionIndex < 0) positionIndex = 3;
		character.destination = characterPositions[positionIndex];

		faceTarget();
	}
	
	// Dog and player only
	public void MoveRight()
	{
		// Cycle through position index and set the destination
		positionIndex++;
		if (positionIndex > 3) positionIndex = 0;
		character.destination = characterPositions[positionIndex];

		faceTarget();
    }
	
	// Bear only
	public void TurnLeft()
	{
		// Cycle through position index and set the destination
		positionIndex++;
		if (positionIndex > 3) positionIndex = 0;
		faceTarget();
	}
	
	// Bear only
	public void TurnRight()
	{
		// Cycle through position index and set the destination
		positionIndex--;
		if (positionIndex < 0) positionIndex = 3;
		faceTarget();
	}
	
	// Gradually turn the dog towards the bear
	public void faceBear()
	{
		Vector3 moveDirection = bear.transform.position - character.transform.position;
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
		targetAngel = angle; 
		//character.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
	}
	
	// Gradually turn bear towards target
	public void faceTarget()
	{
		Vector3 moveDirection = characterPositions[positionIndex] - character.transform.position;
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        targetAngel = angle;
        //character.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
