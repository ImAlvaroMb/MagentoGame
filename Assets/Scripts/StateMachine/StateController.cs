using UnityEngine;
namespace StateMachine
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private State firstState;
        public State CurrentState => currentState;

        private State currentState;

        private void Start()
        {
            ChangeState(firstState);
        }

        private void FixedUpdate()
        {
            currentState.UpdateState();
            State newState = currentState.CheckTransitions();

            if(newState != null) ChangeState(newState);
               
        }

        private void ChangeState(State newState)
        {
            if(currentState != null)
            {
                currentState.OnExit();
                Destroy(currentState);
            }
            currentState = Instantiate(newState);
            currentState.setStateController(this);
            currentState.OnEnter();
        }
    }
}

