using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
	// Reference to player and bear gameobject
	public GameObject bear;

	[SerializeField] float rotateTime = .75f;
	float targetAngel;
	float currentVelocity;
	float CurrentAngle;

    // Update is called once per frame
    void Update()
    {
		faceBear();
        CurrentAngle = Mathf.SmoothDampAngle(CurrentAngle, targetAngel, ref currentVelocity, rotateTime);
		transform.rotation = Quaternion.AngleAxis(CurrentAngle, Vector3.up);
    }
	
	public void faceBear()
	{
		Vector3 moveDirection = bear.transform.position - transform.position;
		float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
		targetAngel = angle; 
		//character.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
	}
}
