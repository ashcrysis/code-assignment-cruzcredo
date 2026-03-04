using UnityEngine;

namespace Player.States
{
    public class IdleState : PlayerState
    {
        public IdleState(PlayerController player, PlayerStateMachine sm)
            : base(player, sm) {}

        public override void Update()
        {
            if (player.Input.MoveInput != Vector2.zero)
                stateMachine.ChangeState(player.MoveState);

            if (player.Input.AttackPressed)
                stateMachine.ChangeState(player.CombatState);

            if (player.Input.DashPressed)
                stateMachine.ChangeState(player.DashState);
        }
    }
}