using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="States/Player/RepulseState")]
    public class PlayerRepulseState : PlayerMoveState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            magnetFieldController.ChangeMagnetMode(Enums.MagnetismForceMode.REPULSE);
        }

        public override void OnExit()
        {
            base.OnExit();
            inputController.EnsureRepulseIsOff();

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

