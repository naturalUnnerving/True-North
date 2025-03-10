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
	
	// Position layer
	public bool far;
	public bool middle;
	public bool near;

	
    // Start is called before the first frame update
    void Start()
    {
        // Set dog positions for attack mode, each of which are located on the cardinal directions of the bear,
		// located 15 units from the bear positon (we can change this later)
		Vector3 bearFrontFar = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z - 15f);
		Vector3 bearBackFar = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z + 15f);
		Vector3 bearLeftFar = new Vector3(bear.transform.position.x + 15f, bear.transform.position.y, bear.transform.position.z);
		Vector3 bearRightFar = new Vector3(bear.transform.position.x - 15f, bear.transform.position.y, bear.transform.position.z);
		Vector3 bearFrontMiddle = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z - 7.5f);
		Vector3 bearBackMiddle = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z + 7.5f);
		Vector3 bearLeftMiddle = new Vector3(bear.transform.position.x + 7.5f, bear.transform.position.y, bear.transform.position.z);
		Vector3 bearRightMiddle = new Vector3(bear.transform.position.x - 7.5f, bear.transform.position.y, bear.transform.position.z);
		Vector3 bearFrontNear = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z - 2f);
		Vector3 bearBackNear = new Vector3(bear.transform.position.x, bear.transform.position.y, bear.transform.position.z + 2f);
		Vector3 bearLeftNear = new Vector3(bear.transform.position.x + 2f, bear.transform.position.y, bear.transform.position.z);
		Vector3 bearRightNear = new Vector3(bear.transform.position.x - 2f, bear.transform.position.y, bear.transform.position.z);
		characterPositions.Add(bearFrontFar); // pos 0
		characterPositions.Add(bearLeftFar); // pos 1
		characterPositions.Add(bearBackFar); // pos 2
		characterPositions.Add(bearRightFar); // pos 3
		characterPositions.Add(bearFrontMiddle); // pos 4
		characterPositions.Add(bearLeftMiddle); // pos 5
		characterPositions.Add(bearBackMiddle); // pos 6
		characterPositions.Add(bearRightMiddle); // pos 7
		characterPositions.Add(bearFrontNear); // pos 8
		characterPositions.Add(bearLeftNear); // pos 9
		characterPositions.Add(bearBackNear); // pos 10
		characterPositions.Add(bearRightNear); // pos 11
		
		// Initialize dog starting position to be next to player
		positionIndex = 0;
		far = true;
		middle = false;
		near = false;

		CurrentAngle = character.transform.eulerAngles.y;

		//faceBear();
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
		if (far && !middle && !near)
		{
			far = false;
			middle = true;
			near = false;
			positionIndex = positionIndex + 4;
		}
		else if (!far && middle && !near)
		{
			far = false;
			middle = false;
			near = true;
			positionIndex = positionIndex + 4;
			
		}
		else if (!far && !middle && near)
		{
			Debug.Log("Can't get any closer!");
		}
		character.destination = characterPositions[positionIndex];
		faceBear();
    }
	
	// Dog only
	public void MoveDown()
	{
		// Return to original outer position
		if (far && !middle && !near)
		{
			Debug.Log("No running away!");
		}
		else if (!far && middle && !near)
		{
			far = true;
			middle = false;
			near = false;
			positionIndex = positionIndex - 4;
		}
		else if (!far && !middle && near)
		{
			far = false;
			middle = true;
			near = false;
			positionIndex = positionIndex - 4;
		}
		character.destination = characterPositions[positionIndex];
		faceTarget();
    }
	
	// Dog and player only
	public void MoveLeft()
	{
		// Cycle through position index and set the destination
		if (far && !middle && !near)
		{
			positionIndex--;
			if (positionIndex < 0) positionIndex = 3;
		}
		else if (!far && middle && !near)
		{
			positionIndex--;
			if (positionIndex < 4) positionIndex = 7;
		}
		else if (!far && !middle && near)
		{
			positionIndex--;
			if (positionIndex < 8) positionIndex = 11;
		}
		character.destination = characterPositions[positionIndex];
		

		faceTarget();
	}
	
	// Dog and player only
	public void MoveRight()
	{
		// Cycle through position index and set the destination
		if (far && !middle && !near)
		{
			positionIndex++;
			if (positionIndex > 3) positionIndex = 0;
		}
		else if (!far && middle && !near)
		{
			positionIndex++;
			if (positionIndex > 7) positionIndex = 4;
		}
		else if (!far && !middle && near)
		{
			positionIndex++;
			if (positionIndex > 11) positionIndex = 8;
		}
		character.destination = characterPositions[positionIndex];
		

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
		// Stop running animation
		/*
		if (anim != null)
		{
			Debug.Log("Stop Running");
			anim.SetTrigger("StopRunning");
		}
		*/
		Vector3 moveDirection = characterPositions[positionIndex] - character.transform.position;
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        targetAngel = angle;
        //character.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
