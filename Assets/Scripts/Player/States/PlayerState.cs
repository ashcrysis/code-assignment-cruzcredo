namespace Player.States
{
    public abstract class PlayerState
    {
        protected PlayerController player;
        protected PlayerStateMachine stateMachine;
        
        public PlayerState(PlayerController player, PlayerStateMachine stateMachine)
        {
            this.player = player;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            string stateName = GetType().Name.Replace("State", "");
            player.anim.SetBool(stateName, true);
        }

        public virtual void Exit()
        {
            string stateName = GetType().Name.Replace("State", "");
            player.anim.SetBool(stateName, false);
        }
        public virtual void Update() { }
    }
}