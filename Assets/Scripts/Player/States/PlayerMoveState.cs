using Enums;
using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName = "States/Player/PlayerMove")]
    public class PlayerMoveState : PlayerBaseState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            animatorController.NotifyBoolAnimationChange(PlayerAnimations.MOVING, true);
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
            movementBehaviour.HandleDirection(
                inputController.MoveInput.x,
                playerStats.MaxSpeed,
                playerStats.Acceleration,
                playerStats.GroundDeceleration,
                playerStats.AirDeceleration
                );

            movementBehaviour.HandleGravity(
                playerStats.FallAcceleration,
                playerStats.MaxFallSpeed,
                playerStats.GroundingForce,
                playerStats.JumpEndEarlyGravityModifier
                );
        }

        public override void UpdateState()
        {
            
        }
    }

}
