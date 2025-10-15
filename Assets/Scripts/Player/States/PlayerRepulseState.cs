using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="States/Player/RepulseState")]
    public class PlayerRepulseState : PlayerMoveState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Entered Repulse State");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exit Repulse State");

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

