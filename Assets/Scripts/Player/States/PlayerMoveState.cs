using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName = "States/Player/PlayerMove")]
    public class PlayerMoveState : PlayerBaseState
    {
        protected MovementBehaviour movementBehaviour;
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void FinishState()
        {
            
        }

        public override void FixedUpdateState()
        {
            movementBehaviour.HandleDirection(
                moveX: inputController.MoveInput.x,
                maxSpeed: playerStats.MaxSpeed,
                acceleration: playerStats.Acceleration,
                groundDeceleration: playerStats.GroundDeceleration,
                airDeceleration: playerStats.AirDeceleration
                );

            movementBehaviour.HandleGravity(
                fallAcceleration: playerStats.FallAcceleration,
                maxFallSpeed: playerStats.MaxFallSpeed,
                groundingForce: playerStats.GroundingForce,
                jumpEndEarlyGravityModifier: playerStats.JumpEndEarlyGravityModifier
                );
        }

        public override void UpdateState()
        {
            
        }
    }

}
