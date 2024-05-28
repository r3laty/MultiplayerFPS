using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [Space]
    [SerializeField] private float speed = 400;
    [SerializeField] private float sprintSpeed = 500;

    [SerializeField] private float jumpForce = 10f; //200

    private Rigidbody _rb;

    private Vector3 _move;
    private Vector2 _input;

    private bool _isSprinting;

    private bool _isJumped;
    private bool _isGrounded;
    private bool _jumped;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, _rb.velocity.y, z);

        Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
        _move = Quaternion.LookRotation(forward) * move;

        _isSprinting = Input.GetButton("Sprint");
        _isJumped = Input.GetButton("Jump");
    }
    private void FixedUpdate()
    {
        if (_isJumped && _isGrounded)
        {
            Jump();
        }

        Move();
    }
    private void Move()
    {
        if (_isSprinting)
        {
            _rb.velocity = new Vector3(_move.x * sprintSpeed * Time.fixedDeltaTime, _move.y, _move.z * sprintSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _rb.velocity = new Vector3(_move.x * speed * Time.fixedDeltaTime, _move.y, _move.z * speed * Time.fixedDeltaTime);
        }
    }
    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
