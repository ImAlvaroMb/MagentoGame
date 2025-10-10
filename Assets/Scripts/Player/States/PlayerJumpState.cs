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
            _timeJumpWasPressed = playerController.CurrentTime;

            movementBehaviour.ExecuteJump(playerStats.JumpPower);
            inputController.ConsumeJumpPressed();
        }

        public override void OnExit()
        {
            base.OnExit();
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

