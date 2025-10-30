using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="States/Player/MagnetOffState")]
    public class PlayerMagnetOffState : PlayerBaseState
    {

        public override void OnEnter()
        {
            base.OnEnter();
            movementBehaviour.NotifyIsOnMagnetismMode(false);
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

