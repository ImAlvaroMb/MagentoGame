using UnityEngine;

namespace StateMachine
{
    [CreateAssetMenu(menuName = "States/Player/PlayerDashing")]
    public class PlayerDashState : PlayerBaseState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            movementBehaviour.HandleDash(inputController.MoveInput.x, (animatorController.GetPlayerRotation() == 0) ? 0f : -1f , playerStats.DashAcceleration, playerStats.MaxDashSpeed);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FinishState()
        {
            
        }

        public override void FixedUpdateState()
        {
            
        }

        public override void UpdateState()
        {
            
        }
    }
}

