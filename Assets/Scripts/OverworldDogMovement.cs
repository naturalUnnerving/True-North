using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldDogMovement : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private Animator anim;	
    private Rigidbody rbody;


    [SerializeField] private float speed;
    [SerializeField] private float distance;
   
    [SerializeField] private float maxDistanceBetweenCharacters;


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
        distance = Vector3.Distance(this.transform.position, player.transform.position);

        
        if (distance >= maxDistanceBetweenCharacters) {
            //Move
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
