using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 500f; //300

    [SerializeField] private float sprintSpeed = 500f; //500

    [SerializeField] private float jumpHeight = 10f; //200

    private Rigidbody _rb;
    private Vector3 _move;

    private bool _isSprinting;

    private bool _isJumped;
    private bool _isGrounded;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _isSprinting = Input.GetButton("Sprint");
        _isJumped = Input.GetButton("Jump");

        if (_isGrounded && _isJumped)
        {
            _rb.AddForce(Vector3.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);
            _isGrounded = false;
        }

        _move = new Vector3(x, _rb.velocity.y, z).normalized;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (_isSprinting)
        {
            Vector3 move = new Vector3(_move.x * sprintSpeed * Time.fixedDeltaTime, _rb.velocity.y, _move.z * sprintSpeed * Time.fixedDeltaTime);
            _rb.velocity = move;
        }
        else
        {
            Vector3 move = new Vector3(_move.x * speed * Time.fixedDeltaTime, _rb.velocity.y, _move.z * speed * Time.fixedDeltaTime);
            _rb.velocity = move;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        _isGrounded = true;
    }
}
