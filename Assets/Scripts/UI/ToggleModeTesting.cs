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
        AttractToggle.enabled = _playerInputController.IsAttractToggleModeOn;
        RepulseToggle.enabled = _playerInputController.IsRepulseToggleModeOn;
    }

    public void ChangeAttractToggleMode()
    {

    }

    
}
