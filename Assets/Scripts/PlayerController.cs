using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _thrust = 10f;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _animator;
    private float _direction;
    private bool _jumpRequested;
    private bool _isJumping;

    void Start()
    {
        _moveAction.action.Enable();
        _jumpAction.action.Enable();
    }

    void Update()
    {
        _direction = _moveAction.action.ReadValue<float>();
        if (_jumpAction.action.WasPressedThisFrame())
        {
            _jumpRequested = true;
        }
        UpdateSprite();
    }

    void FixedUpdate()
    {
        _rb.linearVelocityX = _direction * _speed;

        if (_jumpRequested == true && !_isJumping)
        {
            _rb.AddForce(transform.up * _thrust, ForceMode2D.Impulse);
            _isJumping = true;
        }
    }

    void UpdateSprite()
    {
        if (_direction > 0)
        {
            _renderer.flipX = false;
            _animator.SetBool("isRunning", true);
        } 
        else if (_direction < 0)
        {
            _renderer.flipX = true;
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            _isJumping = false;
            _jumpRequested = false;
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 0);
        }
    }
}
