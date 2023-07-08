using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    public float moveSpeed = 6f; // Speed of the character
    public float turnSmoothTime = 0.1f; // Time to turn to the desired direction
    private float turnSmoothVelocity; // Current turning velocity
    public float jumpForce = 5f; // Force with which the character jumps
    private bool isJumping = false;
    private Rigidbody rb;

    private Camera mainCamera;
    public RotateStaff rotatingObject; // Reference to the rotating object script

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // Horizontal input (A/D or left/right arrow keys)
        float vertical = Input.GetAxisRaw("Vertical"); // Vertical input (W/S or up/down arrow keys)

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
