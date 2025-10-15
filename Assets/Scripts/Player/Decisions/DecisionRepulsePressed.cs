using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="Decision/IsRepuslePressed")]
    public class DecisionRepulsePressed : Decision
    {
        public override bool Decide(StateController stateController)
        {
            return stateController.GetComponent<PlayerInputController>().RepulsePressed;
        }
    }
}

