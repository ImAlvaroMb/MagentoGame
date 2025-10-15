using UnityEngine;
using UnityEngine.UI;

public class ToggleModeTesting : MonoBehaviour
{
    public Toggle AttractToggle;
    public Toggle RepulseToggle;

    private PlayerInputController _playerInputController;

    void Start()
    {
        _playerInputController = PlayerInputController.Instance;
        AttractToggle.isOn = _playerInputController.IsAttractToggleModeOn;
        RepulseToggle.isOn = _playerInputController.IsRepulseToggleModeOn;

        AttractToggle.onValueChanged.AddListener(ChangeAttractToggleMode);
        RepulseToggle.onValueChanged.AddListener(ChangeRepulseToggleMode);
    }

    private void OnEnable()
    {
        _playerInputController = PlayerInputController.Instance;
        AttractToggle.isOn = _playerInputController.IsAttractToggleModeOn;
        RepulseToggle.isOn = _playerInputController.IsRepulseToggleModeOn;
    }

    public void ChangeAttractToggleMode(bool isToggleOn)
    {
        _playerInputController.ChangeAttractMode(isToggleOn);
    }

    public void ChangeRepulseToggleMode(bool isToggleOn)
    {
        _playerInputController.ChangeRepulseMode(isToggleOn);
    }


}
