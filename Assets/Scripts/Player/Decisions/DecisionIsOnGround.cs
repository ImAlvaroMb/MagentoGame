using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="Decision/IsGrounded")]
    public class DecisionIsOnGround : Decision
    {
        public override bool Decide(StateController stateController)
        {
            return stateController.GetComponent<PlayerController>().IsGrounded;
        }
    }
}

