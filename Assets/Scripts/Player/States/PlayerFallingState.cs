using UnityEngine;
using Enums;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="States/Player/PlayerFalling")]
    public class PlayerFallingState : PlayerMoveState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            animatorController.NotifyBoolAnimationChange(PlayerAnimations.FALLING, true);
            animatorController.NotifyBoolAnimationChange(PlayerAnimations.MOVING, false);
        }

        public override void OnExit()
        {
            base.OnExit();
            animatorController.NotifyBoolAnimationChange(PlayerAnimations.FALLING, false);
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
        }

        public override void UpdateState()
        {
           
        }
    }
}

