using UnityEngine;
using Enums;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour // handles most status checks from the player
{
    [HideInInspector] public StatsSO PlayerStats => playerStats;
    [SerializeField] private StatsSO playerStats;

    public Rigidbody2D Rb => _rb;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _collider;
    private MovementBehaviour _movementBehaviour;
    private PlayerAnimatorController _playerAnimatorController;
    private Vector2 _frameVelocity;

    public bool IsGrounded => _isGrounded;
    private bool _isGrounded = false;

    public bool IsFalling => _isFalling;
    private bool _isFalling = false;
    
    private bool _coyoteUsable;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private float _frameLeftGrounded;
    private bool _cachedQueryStartInColliders; //used to not regisetr hits on a collider where the raycast starts from (important to set it back to narmal after changing it since this is global)

    public float CurrentTime => _time;
    private float _time;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _movementBehaviour = GetComponent<MovementBehaviour>();
        _playerAnimatorController = GetComponent<PlayerAnimatorController>();
        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders; // cache initial global value
    }

    private void Update()
    {
        _time = Time.time;
        CheckIfMoving();
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        CheckIfFalling();
    }

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        bool groundHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.down, playerStats.GrounderDistance, ~playerStats.PlayerLayer); // excludes player layer
        bool ceilingHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.up, playerStats.GrounderDistance, ~playerStats.PlayerLayer);
        Debug.DrawLine(_collider.bounds.center, new Vector2(_collider.bounds.center.x , _collider.bounds.center.y) + Vector2.down * playerStats.GrounderDistance, Color.red);

        if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

        if(!_isGrounded && groundHit) //landed
        {
            _isGrounded = true;
        } 
        else if(_isGrounded && !groundHit) // left the ground (jumped)
        {
            _isGrounded = false;
            _frameLeftGrounded = _time;
            // could invoke something like landed or so
        }

        _movementBehaviour.Grounded = _isGrounded;
        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;


        Debug.Log(_isGrounded);
    }

    private void CheckIfFalling()
    {
        if(_rb.linearVelocity.y < -playerStats.FallingSpeedThreshold)
        {
            _isFalling = true;
        } else
        {
            _isFalling = false;
        }
    }

    private void CheckIfMoving()
    {
        if(Mathf.Abs(_rb.linearVelocity.x) > 0.1f)
        {
            _playerAnimatorController.NotifyBoolAnimationChange(PlayerAnimations.MOVING, true);
        } else
        {
            _playerAnimatorController.NotifyBoolAnimationChange(PlayerAnimations.MOVING, false);
        }
    }
}
