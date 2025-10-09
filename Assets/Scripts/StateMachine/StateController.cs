using UnityEngine;
namespace StateMachine
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private State firstState;
        public State CurrentState => _currentState;

        private State _currentState;

        private void Start()
        {
            ChangeState(firstState);
        }

        private void FixedUpdate()
        {
            _currentState.UpdateState();
            State newState = _currentState.CheckTransitions();

            if(newState != null) ChangeState(newState);
               
        }

        private void ChangeState(State newState)
        {
            if(_currentState != null)
            {
                _currentState.OnExit();
                Destroy(_currentState);
            }
            _currentState = Instantiate(newState);
            _currentState.setStateController(this);
            _currentState.OnEnter();
        }
    }
}

