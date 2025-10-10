using UnityEngine;
namespace StateMachine 
{
    [CreateAssetMenu(menuName ="Decision/JumpPressed")]
    public class DecisionJumpPressed : Decision
    {
        public override bool Decide(StateController stateController)
        {
            return stateController.GetComponent<PlayerInputController>().JumpPressed;
        }
    }
}

