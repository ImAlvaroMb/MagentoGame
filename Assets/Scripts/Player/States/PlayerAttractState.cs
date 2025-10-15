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
            Debug.Log("Entered Attract State");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exit Attract State");
            
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
