using UnityEngine;
namespace StateMachine
{
    public abstract class State : ScriptableObject
    {
        protected StateController stateControler;
        [SerializeField] private Transition[] possibleTransitions;
        public bool IsDone => isDone;

        private bool isDone = false;

        public abstract void OnEnter();
        public abstract void UpdateState();
        public abstract void OnExit();
        public abstract void FinishState();

        public void setStateController(StateController stateController)
        {
            this.stateControler = stateController;
        }

        public State CheckTransitions()
        {
            State exitState = null;
            for (int i = 0; i < possibleTransitions.Length && exitState != null; i++)
            {
                exitState = possibleTransitions[i].getExitState(stateControler);
            }

            return exitState;
        }
    }

}
