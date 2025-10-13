using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName = "Decision/IfCanDash")]
    public class DecisionDashPressedAndIfCanDash : Decision
    {
        public override bool Decide(StateController stateController)
        {
            if(stateController.GetComponent<MovementBehaviour>().CanDash && stateController.GetComponent<PlayerInputController>().DashPressed)
            {
                return true;
            } else
            {
                return false;
            }
            
        }
    }
}

