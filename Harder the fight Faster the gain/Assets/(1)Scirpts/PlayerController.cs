using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _moveSpeed = 5f;
    Vector2 _moveInput;
    Vector3 _direction;



    [Header("Gravity&Jump")]
    [SerializeField] float _jumpStrength = 3;
    [SerializeField] float _gravityFactor = 1f;
    float _gravity = -9.81f;//real life gravity = 9.82 in swedeon 9.81 elsewher
    float _velocity;
    [SerializeField] int _extraJumps = 0;
    int _jumpsLeft;



    CharacterController _characterController;



    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {

        ApplyGravity();
        Move();
        if (isGrounded()) { _jumpsLeft = _extraJumps; }

    }
    private void FixedUpdate()
    {

    }

    public void MoveInputRead(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        Vector3 localDirection = new Vector3(_moveInput.x, 0, _moveInput.y);

        _direction = transform.TransformDirection(localDirection).normalized;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!isGrounded() && _jumpsLeft <= 0) return;
        if (isGrounded()) { _velocity = _jumpStrength; }
        else { _velocity = _jumpStrength; _jumpsLeft--; }
     
       
    }
    void ApplyGravity()
    {
        if (isGrounded() && _velocity < 0)
        {
            _velocity = -1f;
        }
        else
        {
            _velocity += _gravity * _gravityFactor * Time.deltaTime;

        }
        _direction.y = _velocity;
        
    }

    void Move()
    {
        _characterController.Move(_direction * _moveSpeed * Time.deltaTime);
    }

    private bool isGrounded() => _characterController.isGrounded;
}
