using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour // handles inputs
{
    private InputSystem_Actions _actions;
    private bool _isInputBlocked = false;


    public Vector2 MoveInput { get; private set; }

    public bool JumpPressed { get; private set; }
    public bool JumpHold { get; private set; }
    public float JumpHoldTime { get; private set; }
    
    private float jumpStartTime;
    public bool DashPressed { get; private set; }
    

    private void Awake()
    {
        _actions = new InputSystem_Actions();

        _actions.Player.Move.performed += OnMovePerformed;
        _actions.Player.Move.canceled -= OnMovePerformed;

        _actions.Player.Jump.performed += OnJumpPermormed;
        _actions.Player.Jump.canceled += OnJumpCanceled;

        _actions.Player.Dash.performed += ctx => DashPressed = true;
        _actions.Player.Dash.canceled += ctx => DashPressed = false;
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    private void Update()
    {
        if(JumpHold) JumpHoldTime = Time.time - jumpStartTime;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnJumpPermormed(InputAction.CallbackContext context)
    {
        JumpPressed = true;
        JumpHold = true;
        jumpStartTime = Time.time;
        JumpHoldTime = 0f;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        JumpHold = false;
        JumpHoldTime = 0f;
    }
}
