using Enums;
using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="States/Player/AttractState")]
    public class PlayerAttractState : PlayerMoveState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            magnetFieldController.ChangeMagnetMode(MagnetismForceMode.ATTRACT);
        }

        public override void OnExit()
        {
            base.OnExit();
            inputController.EnsureAttractIsOff();
            
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
        }

        public override void UpdateState()
        {
            magnetFieldController.UpdateAimDirection(inputController.LookInput, inputController.IsUsingController);
            magnetFieldController.CalculateForcesToBeApplied();
        }
    }

}
