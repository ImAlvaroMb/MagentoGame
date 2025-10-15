using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="Decision/IsAttractPressed")]
    public class DecisionAttractPressed : Decision
    {
        public override bool Decide(StateController stateController)
        {
            return stateController.GetComponent<PlayerInputController>().AttractPressed;
        }
    }
}

