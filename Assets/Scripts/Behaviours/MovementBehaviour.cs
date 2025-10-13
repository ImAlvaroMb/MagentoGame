using UnityEngine;
using Utilities;
using Enums;

public class MovementBehaviour : MonoBehaviour
{
    private PlayerController _playerController;

    private Rigidbody2D _rb;
    private Vector2 _frameVelocity;

    [HideInInspector] public bool Grounded = false;
    [HideInInspector] public bool EndedJumpEarly = false;
    [HideInInspector] public float TimeLeftGrounded = float.MinValue;

    public bool CanDash => _canDash;
    private bool _canDash = true;
    public bool IsDashing => _isDashing;
    private bool _isDashing = false;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void ExecuteJump(float jumpPower)
    {
        EndedJumpEarly = false;
        _frameVelocity.y = jumpPower;
        // possible event that we jumped
    }

    public void HandleDirection(float moveX, float maxSpeed, float acceleration, float groundDeceleration, float airDeceleration)
    {
        if(moveX == 0)
        {
            var deceleration = Grounded ? groundDeceleration : airDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0 , deceleration * Time.fixedDeltaTime);
        } else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, moveX * maxSpeed, acceleration * Time.fixedDeltaTime);
        }
    }

    public void CheckForJumpEndEalry(bool jumpButtonHeld)
    {
        if (EndedJumpEarly) return;

        if(!Grounded && _rb.linearVelocity.y > 0f && !jumpButtonHeld)
        {
            EndedJumpEarly = true;
        }
    }

    public void HandleGravity(float fallAcceleration, float maxFallSpeed, float groundingForce, float jumpEndEarlyGravityModifier)
    {
        if(Grounded && _frameVelocity.y <= 0)
        {
            //_frameVelocity.y = groundingForce;
            _frameVelocity.y = 0f;
        } else
        {
            var inAirGravity = fallAcceleration;

            if(EndedJumpEarly && _frameVelocity.y > 0) // jump was cut short
            {
                inAirGravity *= jumpEndEarlyGravityModifier;
            }

            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    public void HandleDash(float moveX, float originalDirection, float dashSpeed, float maxDashSpeed) 
    {
        if(_canDash)
        {
            _canDash = false;
            _isDashing = true;
            float direction = 0f;
            if (moveX != 0f) direction = Mathf.Sign(moveX);
            else direction = Mathf.Sign(originalDirection);

            TimerSystem.Instance.CreateTimer(_playerController.PlayerStats.DashDuration, onTimerDecreaseComplete: () =>
            {
                _frameVelocity.x = 0f;
                _isDashing = false;
                TimerSystem.Instance.CreateTimer(_playerController.PlayerStats.DashCooldown, onTimerDecreaseComplete: () => _canDash = true);
            }, onTimerDecreaseUpdate: (progress) =>
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, direction * maxDashSpeed, dashSpeed * progress);
                _frameVelocity.y = 0f;
            });
        }
    }

    private void ApplyMovement()
    {
        _rb.linearVelocity = _frameVelocity;
    }
}
