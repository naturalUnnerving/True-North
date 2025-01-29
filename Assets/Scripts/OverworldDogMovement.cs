using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldDogMovement : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private Animator anim;	
    private Rigidbody rbody;

    //We will use the x and z parameters to determine how far the player is from the dog.
    [SerializeField] private Vector3 maxDistanceBetweenCharacters;


    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        rbody = GetComponentInChildren<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponentInChildren<Rigidbody>().velocity != Vector3.zero) {
            //Move
        }
    }
}
