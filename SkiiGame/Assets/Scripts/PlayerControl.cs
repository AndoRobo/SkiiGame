using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;
    
    private InputAction move;
    [SerializeField] private float turnSpeed = 10;
    [SerializeField] private float speed = 10;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Vector3 obstacleknockback; 
    Rigidbody rb;


    private void OnEnable()
    {
        Obstacle.OnObstacleHit += OnCollision;
    }

    private void OnDisable()
    {
        Obstacle.OnObstacleHit -= OnCollision;
    }

    private void OnCollision()
    {
        Debug.Log("HIITHIT");
        rb.AddForce(obstacleknockback,ForceMode.Impulse);
    }

    private void Awake()
    {
        Instance = this;
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        bool isGrounded = Physics.Linecast(transform.position, transform.position - Vector3.up, ground);
        if (isGrounded)
        {
            Vector2 moveVector = move.ReadValue<Vector2>();
            Debug.Log("move x " + moveVector.x + "move y " + moveVector.y);
            float rotationSpeed = -moveVector.x * turnSpeed * Time.fixedDeltaTime;
            rb.AddTorque(new Vector3(0, rotationSpeed, 0));
        }

        float speedMultiplier = Math.Abs(Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));
            rb.AddForce(transform.forward * speed * speedMultiplier * Time.fixedDeltaTime);
        
    }
}
