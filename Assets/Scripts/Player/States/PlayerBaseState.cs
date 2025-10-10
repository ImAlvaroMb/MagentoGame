using UnityEngine;

namespace StateMachine
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerInputController inputController;
        protected PlayerController playerController;
        protected MovementBehaviour movementBehaviour;
        protected StatsSO playerStats;
        public float BlockControlEntryThreshold;
        public float BlockControlExitThreshold;
        public override void OnEnter()
        {
            inputController = stateController.gameObject.GetComponent<PlayerInputController>();
            playerController = stateController.gameObject.GetComponent<PlayerController>();
            movementBehaviour = stateController.gameObject.GetComponent<MovementBehaviour>();
            playerStats = playerController.PlayerStats;
        }

        public override void OnExit() { }

        public abstract override void FixedUpdateState();

        public abstract override void UpdateState();
       
    }
}

