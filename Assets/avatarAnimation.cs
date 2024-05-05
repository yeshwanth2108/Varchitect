using UnityEngine;

public class avatarAnimation : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // Movement speed of the avatar
    [SerializeField]
    private float rotationSpeed = 120f; // Rotation speed for turning the avatar

    private Animator animator; // Animator component reference
    private CharacterController characterController; // CharacterController component reference
    private float verticalVelocity; // Current vertical velocity (for gravity)
    private GameObject first_hit;

    // Initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        Vector3 ray_obj = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(ray_obj, Camera.main.transform.forward, out RaycastHit hit, 500f))
        {
            first_hit = hit.collider.gameObject;
            if (first_hit.name.Contains("main_door"))
            {
                animator.SetBool("opendoor", true);
            }
            else{
                animator.SetBool("opendoor", false);
            }
        }

    }

    private void HandleMovement()
    {
        // Get input from horizontal and vertical axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction and normalize it
        Vector3 movementDirection = new Vector3(-horizontalInput, 0, -verticalInput).normalized;

        // Apply gravity to vertical velocity
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        // Calculate the final velocity vector
        Vector3 velocity = movementDirection * (Mathf.Clamp01(movementDirection.magnitude) * speed);
        velocity.y = verticalVelocity;

        // Move the character controller
        characterController.Move(velocity * Time.deltaTime);

        // Handle animations and rotation if there is movement input
        if (movementDirection != Vector3.zero)
        {
            SetMovementAnimation(true);
            RotateTowardsMovementDirection(movementDirection);
        }
        else
        {
            SetMovementAnimation(false);
        }
    }

    private void SetMovementAnimation(bool isMoving)
    {
        animator.SetBool("move", isMoving);
    }

    private void RotateTowardsMovementDirection(Vector3 movementDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}