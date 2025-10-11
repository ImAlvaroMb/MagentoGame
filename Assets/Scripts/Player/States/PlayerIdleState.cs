using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName = "States/Player/PlayerIdle")]
    public class PlayerIdleState : PlayerBaseState
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void FinishState()
        {
            
        }

        public override void FixedUpdateState()
        {
            movementBehaviour.HandleDirection( // handle deaceleration
               0f,
               playerStats.MaxSpeed,
               playerStats.Acceleration,
               playerStats.GroundDeceleration,
               playerStats.AirDeceleration
               );
        }

        public override void UpdateState()
        {
            
        }
    }

}
