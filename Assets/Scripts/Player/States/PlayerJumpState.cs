using Enums;
using UnityEngine;

namespace StateMachine
{
    [CreateAssetMenu(menuName = "States/Player/PlayerJump")]
    public class PlayerJumpState : PlayerMoveState
    {
        private bool _jumpToConsume = true;
        private float _timeJumpWasPressed;

        public override void OnEnter()
        {
            base.OnEnter();
            animatorController.NotifyBoolAnimationChange(PlayerAnimations.JUMPING, true);
            animatorController.NotifyBoolAnimationChange(PlayerAnimations.MOVING, false);
            _timeJumpWasPressed = playerController.CurrentTime;

            movementBehaviour.ExecuteJump(playerStats.JumpPower);
            inputController.ConsumeJumpPressed();
        }

        public override void OnExit()
        {
            base.OnExit();
            animatorController.NotifyBoolAnimationChange(PlayerAnimations.JUMPING, false);
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
        }

        public override void UpdateState()
        {
            movementBehaviour.CheckForJumpEndEalry(inputController.JumpHold);
        }
    }
}

