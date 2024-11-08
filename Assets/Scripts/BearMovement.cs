using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearMovement : MonoBehaviour
{
	// Initialize player positions list
	[SerializeField] private List<Vector3> playerPositions = new List<Vector3>();
	// Link bear navmesh angent
	[SerializeField] private NavMeshAgent bear;
	// Index for bear positions list
	[SerializeField] private int positionIndex;
	// Reference to player gameobject
	public GameObject player;
	
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
		
		// Initialize bear orientation
		positionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
		/* TESTING, NOT FINAL*/
		
        // Using Z for left and C for right, move through the bear positions. try not to spam >:]
		if (Input.GetKeyDown("c"))
		{
			// Cycle through position index and set the destination
			positionIndex--;
			if (positionIndex < 0) positionIndex = 3;
			faceTarget();
		}
		
		if (Input.GetKeyDown("z"))
		{
			// Cycle through position index and set the destination
			positionIndex++;
			if (positionIndex > 3) positionIndex = 0;
			faceTarget();
		}
    }
	
	// Gradually turn bear towards target
	void faceTarget()
	{
		Vector3 moveDirection = playerPositions[positionIndex];
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
		bear.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
	}
}
