using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private InputSystem_Actions actions;
    private bool isInputBlocked = false;


    public Vector2 MoveInput { get; private set; }

    public bool JumpPressed { get; private set; }
    public bool JumpHold { get; private set; }
    public float JumpHoldTime { get; private set; }
    
    private float jumpStartTime;
    public bool DashPressed { get; private set; }
    

    private void Awake()
    {
        actions = new InputSystem_Actions();

        actions.Player.Move.performed += OnMovePerformed;
        actions.Player.Move.canceled -= OnMovePerformed;

        actions.Player.Jump.performed += OnJumpPermormed;
        actions.Player.Jump.canceled += OnJumpCanceled;

        actions.Player.Dash.performed += ctx => DashPressed = true;
        actions.Player.Dash.canceled += ctx => DashPressed = false;
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
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
