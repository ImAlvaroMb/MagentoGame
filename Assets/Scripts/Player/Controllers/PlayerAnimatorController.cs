using UnityEngine;
using Enums;
public class PlayerAnimatorController : MonoBehaviour
{
    public Animator animatorController;
    public GameObject playerObj;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        HandleSpriteRotation();
    }

    private void HandleSpriteRotation()
    {
        if (_playerController.Rb.linearVelocity.x > 0.05f) playerObj.transform.rotation = Quaternion.Euler(0, 0, 0);
       
        if (_playerController.Rb.linearVelocity.x < -0.05f) playerObj.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void NotifyBoolAnimationChange(PlayerAnimations typeToNotify, bool value)
    {
        animatorController.SetBool(typeToNotify.ToString(), value);
    }
}
