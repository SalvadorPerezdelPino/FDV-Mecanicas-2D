using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _floorLayer;
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _thrust = 10f;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _animator;
    //[SerializeField] private float _checkDistance = 0.05f;
    private float _direction;
    private bool _jumpRequested;
    private bool _isJumping;

    private Vector2 _raycastOrigin;
    void Start()
    {
        _moveAction.action.Enable();
        _jumpAction.action.Enable();
    }

    void Update()
    {
        _direction = _moveAction.action.ReadValue<float>();
        if (_jumpAction.action.WasPressedThisFrame() && !_isJumping)
        {
            _jumpRequested = true;
        }

        /*_raycastOrigin = new Vector2(_collider.bounds.center.x, _collider.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(_raycastOrigin, -transform.up, _checkDistance, _floorLayer);

        if (hit.collider != null && hit.collider.CompareTag("Floor"))
        {
            _isJumping = false;
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 0);
        }*/

        UpdateSprite();
    }

    void FixedUpdate()
    {
        _rb.linearVelocityX = _direction * _speed;

        if (_jumpRequested == true && !_isJumping)
        {
            _rb.AddForce(transform.up * _thrust, ForceMode2D.Impulse);
            _isJumping = true;
            _jumpRequested = false;
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
            StopJump();
        } 
        else if (collision.collider.CompareTag("StaticPlatform"))
        {
            StopJump();
            transform.SetParent(collision.collider.gameObject.transform);
        }
        else if (collision.collider.CompareTag("MovingPlatform"))
        {
            StopJump();
            transform.SetParent(collision.collider.gameObject.transform);
        }
    }

    private void StopJump()
    {
        _isJumping = false;
        _jumpRequested = false;
        _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 0);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.SetParent(null);
    }
}
