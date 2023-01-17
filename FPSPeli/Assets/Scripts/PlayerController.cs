using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private float canJump = 0f;
    Vector3 velocity;
    bool isGrounded;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // This checks if the player is standing on ground or how far the player is from ground

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 11f; // Increase speed when you begin sprinting
            // Debug.Log("sprinting");
        }
        else // Reduce speed when you stop sprinting
        {
            speed = 6f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Horizontal"))
        {
            animator.SetBool("isMovingForward", true);
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            animator.SetBool("isMovingForward", false);
        } 

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded && Time.time > canJump) // checks if the player is on ground before allowing to jump
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("IsJumping", true);
            Debug.Log("Jumping");
            canJump = Time.time + 1f;
        }
        if (Input.GetButtonUp("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", false);
            Debug.Log("Not Jumping");
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
