using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="Decision/IsFalling")]
    public class DecisionIsFalling : Decision
    {
        public override bool Decide(StateController stateController)
        {
            PlayerController playerController = stateController.GetComponent<PlayerController>();
            if(!playerController.IsGrounded && playerController.IsFalling)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }

}
