using UnityEngine;

namespace StateMachine
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerInputController inputController;
        protected PlayerController playerController;
        protected StatsSO playerStats;
        public float BlockControlEntryThreshold;
        public float BlockControlExitThreshold;
        public override void OnEnter()
        {
            inputController = stateControler.GetComponent<PlayerInputController>();
            playerController = stateControler.GetComponent<PlayerController>();
            playerStats = playerController.PlayerStats;
        }

        public override void OnExit() { }

        public abstract override void FixedUpdateState();

        public abstract override void UpdateState();
       
    }
}

