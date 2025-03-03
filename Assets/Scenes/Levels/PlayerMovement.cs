using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private Transform cameraTransform;
    private bool isGrounded;
    private float xRotation = 0f; // Store camera's vertical rotation

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundDistance = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();
        Jump();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");  // A & D
        float moveZ = Input.GetAxis("Vertical");    // W & S

        Vector3 moveDirection = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        moveDirection.y = 0; // Prevent movement from tilting up/down

        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player left/right
        transform.Rotate(Vector3.up * mouseX);

        // Adjust vertical rotation and clamp it
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Prevent camera flipping

        // Apply rotation to the camera
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}
