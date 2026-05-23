using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float turnForce = 25f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 25f;
    [SerializeField] private float maxAcceleration = 30f;
    [SerializeField] private float minAcceleration = -10f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundCheck;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private Rigidbody rb;

    private InputAction moveAction;

    private float horizontalInput;
    private float speed;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Player/Move");

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        // INPUT
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        horizontalInput = moveInput.x;

        // GROUND CHECK
        isGrounded = Physics.Linecast(
            transform.position,
            groundCheck.position,
            groundLayers
        );
    }

    private void FixedUpdate()
    {
        HandleTurning();
        HandleMovement();
        HandleAnimation();
    }

    private void HandleTurning()
    {
        if (!isGrounded) return;

        float yRotation = transform.eulerAngles.y;

        // Convert 0-360 to -180 / 180
        if (yRotation > 180)
        {
            yRotation -= 360;
        }

        bool canTurnLeft =
            !(yRotation <= -90 && horizontalInput < 0);

        bool canTurnRight =
            !(yRotation >= 90 && horizontalInput > 0);

        if (canTurnLeft && canTurnRight)
        {
            rb.AddTorque(Vector3.up * horizontalInput * turnForce);
        }
    }

    private void HandleMovement()
    {
        // 180 = downhill
        float angle = Mathf.Abs(transform.eulerAngles.y - 180);

        if (angle > 180)
        {
            angle = 360 - angle;
        }

        float acceleration = Remap(
            0,
            90,
            maxAcceleration,
            minAcceleration,
            angle
        );

        speed += acceleration * Time.fixedDeltaTime;

        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        Vector3 velocity = transform.forward * speed;

        rb.linearVelocity = new Vector3(
            velocity.x,
            rb.linearVelocity.y,
            velocity.z
        );
    }

    private void HandleAnimation()
    {
        if (animator == null) return;

        animator.SetFloat("playerSpeed", speed);
    }

    private float Remap(
        float oldMin,
        float oldMax,
        float newMin,
        float newMax,
        float value)
    {
        return newMin + (value - oldMin) *
            (newMax - newMin) /
            (oldMax - oldMin);
    }
}