using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 12;
    public float gravity = 9.8f;
    public float groundCHeckRadius = 0.15f;
    public LayerMask groundLayer;//physic check

    private bool isGrounded;
    private Vector3 velocity;
    private Transform feet;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        feet = transform.Find("feet");
    }

    private void Update()
    {
        CheckIsGrounded();
        //isGrounded = Physics.CheckSphere(feet.position, groundCHeckRadius, groundLayer);
        Move();
        ApplyGravity();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move);
    }

    private void CheckIsGrounded()
    {
        isGrounded = Physics.CheckSphere(feet.position, groundCHeckRadius, groundLayer);
        Debug.Log($"IsGrounded: {isGrounded}");

    }

    private void ApplyGravity()
    {
        velocity += Vector3.down * gravity * Time.deltaTime;
        if (isGrounded)
            velocity = Vector3.zero;

        controller.Move(velocity);
    }
}