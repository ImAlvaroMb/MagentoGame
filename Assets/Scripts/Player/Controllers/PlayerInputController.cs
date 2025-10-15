using UnityEngine;
using UnityEngine.InputSystem;
using Enums;
using System.Linq;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;
using Utilities;

public class PlayerInputController : AbstractSingleton<PlayerInputController> // handles inputs
{
    public delegate void InputDeviceChanged(InputDeviceType deviceType);
    public static event InputDeviceChanged OnInputDeviceChanged;

    private InputDeviceType _currentInputDeviceType = InputDeviceType.KEYBOARD_MOUSE;
    public InputSystem_Actions Actions => _actions;
    private InputSystem_Actions _actions;
    private bool _isInputBlocked = false;

    public bool IsUsingController => _isUsingController;
    private bool _isUsingController = false;

    public Vector2 MoveInput { get; private set; }

    public bool JumpPressed { get; private set; }
    public bool JumpHold { get; private set; }
    public float JumpHoldTime { get; private set; }
    
    private float jumpStartTime;
    public bool DashPressed { get; private set; }

    public bool AttractPressed { get; private set; }
    public bool AttractHold { get; private set; }
    public bool RepulsePressed { get; private set; }
    public bool RepulseHold { get; private set; }

    public bool IsAttractToggleModeOn => _isAttractToggleModeOn;
    private bool _isAttractToggleModeOn = false;
    public bool IsRepulseToggleModeOn => _isRepulseToggleModeOn;
    private bool _isRepulseToggleModeOn = false;
    

    protected override void Awake()
    {
        _actions = new InputSystem_Actions();
        _actions.Enable();

        _actions.Player.Move.performed += OnMovePerformed;
        _actions.Player.Move.canceled += OnMovePerformed;

        _actions.Player.Jump.performed += OnJumpPermormed;
        _actions.Player.Jump.canceled += OnJumpCanceled;

        _actions.Player.Dash.performed += ctx => DashPressed = true;
        _actions.Player.Dash.canceled += ctx => DashPressed = false;

        _actions.Player.Attract.performed += HandleAttractInput;
        _actions.Player.Attract.canceled += HandleAttractInput;

        _actions.Player.Repulse.performed += HandleRepulseInput;
        _actions.Player.Repulse.canceled += HandleRepulseInput;

        _actions.Player.Pause.performed += PauseManager.Instance.HandlePausePressed;
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
        CheckForInputDeviceChange();
        if(JumpHold) JumpHoldTime = Time.time - jumpStartTime;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        Debug.Log(MoveInput);
    }

    private void OnJumpPermormed(InputAction.CallbackContext context)
    {
        JumpPressed = true;
        JumpHold = true;
        jumpStartTime = Time.time;
        JumpHoldTime = 0f;
        Debug.Log("JumpPressed");
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        JumpHold = false;
        JumpHoldTime = 0f;
    }

    public void ConsumeJumpPressed() // ensure that the player needs to press jump again
    {
        JumpPressed = false;
    }

    private void HandleAttractInput(InputAction.CallbackContext context)
    {
        if(!_isAttractToggleModeOn)
        {
            AttractPressed = context.performed;
            //if (AttractPressed) RepulsePressed = false;
        } else
        {
            AttractPressed = !AttractPressed;
        }
    }

    private void HandleRepulseInput(InputAction.CallbackContext context)
    {
        if(!_isRepulseToggleModeOn)
        {
            RepulsePressed = context.performed;
            //if (RepulsePressed) AttractPressed = false;
        } else
        {
            if(RepulsePressed == true)
            {
                RepulsePressed = false;
            } else
            {
                RepulsePressed = true;
            }

            
        }
    }

    public void ChangeAttractMode(bool value)
    {
        if(value)
        {
            _actions.Player.Attract.canceled -= HandleAttractInput;
        } else
        {
            _actions.Player.Attract.canceled += HandleAttractInput;
        }

        _isAttractToggleModeOn = value;
    } 

    public void ChangeRepulseMode(bool value)
    {
        if(value)
        {
            _actions.Player.Repulse.canceled -= HandleRepulseInput;
        } else
        {
            _actions.Player.Repulse.canceled += HandleRepulseInput;
        }
        
        _isRepulseToggleModeOn = value;
    }  

    private void CheckForInputDeviceChange()
    {
        if(Keyboard.current != null && Keyboard.current.anyKey.isPressed)
        {
            _currentInputDeviceType = InputDeviceType.KEYBOARD_MOUSE;
            _isUsingController = false;
            OnInputDeviceChanged?.Invoke(_currentInputDeviceType);
        }

        bool gamepadUsed = false;
        if(Gamepad.current != null)
        {
            gamepadUsed = Gamepad.current.allControls.Any(x => x is ButtonControl button && x.IsPressed() && !x.synthetic);
        }

        if(gamepadUsed)
        {
            if(Gamepad.current is DualShockGamepad) _currentInputDeviceType = InputDeviceType.PS_CONTROLLER;

            if(Gamepad.current is XInputController) _currentInputDeviceType = InputDeviceType.XBOX_CONTROLLER;

            OnInputDeviceChanged?.Invoke(_currentInputDeviceType);
            _isUsingController = true;
        }
    }

    public InputDeviceType GetCurrentInputDeviceType() => _currentInputDeviceType;
}
