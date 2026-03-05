namespace Player.States
{
    public abstract class PlayerState
    {
        protected PlayerController player;
        protected PlayerStateMachine stateMachine;
        protected string stateName;

        public PlayerState(PlayerController player, PlayerStateMachine stateMachine)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            stateName = GetType().Name.Replace("State", "");
        }

        public virtual void Enter()
        {
            player.anim.SetBool(stateName, true);
        }

        public virtual void Exit()
        {
            player.anim.SetBool(stateName, false);
        }
        public virtual void Update() { }
    }
}