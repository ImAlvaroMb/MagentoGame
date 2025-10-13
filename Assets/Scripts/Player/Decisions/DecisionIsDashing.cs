using UnityEngine;
namespace StateMachine
{
    [CreateAssetMenu(menuName ="Decision/IsDashing")]
    public class DecisionIsDashing : Decision
    {
        public override bool Decide(StateController stateController)
        {
            return stateController.GetComponent<MovementBehaviour>().IsDashing;
        }
    }
}

