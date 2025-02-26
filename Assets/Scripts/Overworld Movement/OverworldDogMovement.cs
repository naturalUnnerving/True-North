using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OverworldDogMovement : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private NavMeshAgent dogNav;

    private GameObject dogTargetPosition;

    private Animator anim;	
    private Rigidbody rbody;


    private float speed;
    //private float distance;
   
    //[SerializeField] private float maxDistanceBetweenCharacters;


    private void Awake() {
        dogNav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody>();
        speed = player.GetComponent<OverworldPlayerController>().MoveSpeed - (player.GetComponent<OverworldPlayerController>().MoveSpeed * .2f);
        dogTargetPosition = player.transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        dogNav.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        dogNav.SetDestination(dogTargetPosition.transform.position);

        /*
        distance = Vector3.Distance(this.transform.position, dogTargetPosition.transform.position);

        
        if (distance >= maxDistanceBetweenCharacters) {
            //Move
            this.transform.position = Vector3.MoveTowards(this.transform.position, dogTargetPosition.transform.position, speed * Time.deltaTime);
        }
        */
    }
}
