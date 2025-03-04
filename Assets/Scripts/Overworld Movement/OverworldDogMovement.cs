using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OverworldDogMovement : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private NavMeshAgent dogNav;

    private GameObject dogTargetPositionGO;

    [SerializeField] private Vector3 dogTargetPosition;

    private Animator anim;
    private int _animIDRunning;
    private Rigidbody rbody;


    [SerializeField] private float speed;

    [Range(-0.5f, 0f)]
    [SerializeField] private float speedAdjustment = .2f;
    //private float distance;
   
    //[SerializeField] private float maxDistanceBetweenCharacters;


    private void Awake() 
    {
        dogNav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody>();
        speed = player.GetComponent<OverworldPlayerController>().MoveSpeed - (player.GetComponent<OverworldPlayerController>().MoveSpeed * speedAdjustment);
        dogTargetPositionGO = player.transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        dogNav.speed = speed;

        AssignAnimationIDs();

    }


    private void AssignAnimationIDs()
    {
        //_animIDSpeed = Animator.StringToHash("Speed");
        //_animIDGrounded = Animator.StringToHash("Grounded");
        //_animIDFreeFall = Animator.StringToHash("FreeFall");
        //_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDRunning = Animator.StringToHash("Running");
    }

    // Update is called once per frame
    void Update()
    {
        dogNav.SetDestination(dogTargetPositionGO.transform.position);

        dogTargetPosition = dogTargetPositionGO.transform.position;

        if(Mathf.Approximately(dogTargetPositionGO.transform.position.z, transform.position.z))
        {
            anim.SetBool(_animIDRunning, false);
        }
        else
        {
            anim.SetBool(_animIDRunning, true);
        }

        /*
        distance = Vector3.Distance(this.transform.position, dogTargetPosition.transform.position);

        
        if (distance >= maxDistanceBetweenCharacters) {
            //Move
            this.transform.position = Vector3.MoveTowards(this.transform.position, dogTargetPosition.transform.position, speed * Time.deltaTime);
        }
        */
    }
}
