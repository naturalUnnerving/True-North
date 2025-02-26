using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class BasicControlScript : MonoBehaviour
{
    //private Animator anim;	
    public Rigidbody rbody;
    public CharacterInputController cinput;

    public float forwardMaxSpeed = 1f;
    public float turnMaxSpeed = 1f;

    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    [SerializeField] private Vector3 velocity;


    void Awake()
    {

        cinput = GetComponent<CharacterInputController>();

        if (cinput == null)
            Debug.Log("CharacterInputController could not be found");

    }

    private void Start() {
        velocity = rbody.velocity;
    }


    void Update() {

        float inputForward=0f;
        float inputTurn=0f;

        if (cinput.enabled)
        {
            inputForward = cinput.Forward;
            inputTurn = cinput.Turn;
        }
        
        //switch turn around if going backwards
        if(inputForward < 0f)
        inputTurn = -inputTurn;

        rbody.MovePosition(rbody.position +  this.transform.forward * inputForward * Time.deltaTime * forwardMaxSpeed);
        
        rbody.MoveRotation(rbody.rotation * Quaternion.AngleAxis(inputTurn * Time.deltaTime * turnMaxSpeed, Vector3.up));

    }

}
