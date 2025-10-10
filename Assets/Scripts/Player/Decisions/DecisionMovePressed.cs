using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName = "Decision/MovePressed")]
    public class DecisionMovePressed : Decision
    {
        public override bool Decide(StateController stateController)
        {
            if(stateController.GetComponent<PlayerInputController>().MoveInput == Vector2.zero)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}

