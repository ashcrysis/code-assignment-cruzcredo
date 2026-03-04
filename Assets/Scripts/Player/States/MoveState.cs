using Player;
using Player.States;
using UnityEngine;

namespace Player.States
{
    public class MoveState : PlayerState
    {
        public MoveState(PlayerController player, PlayerStateMachine sm)
            : base(player, sm)
        {
        }

        public override void Update()
        {
            player.Move();

            if (player.Input.MoveInput == Vector2.zero)
                stateMachine.ChangeState(player.IdleState);

            if (player.Input.AttackPressed)
                stateMachine.ChangeState(player.CombatState);

            if (player.Input.DashPressed)
                stateMachine.ChangeState(player.DashState);
        }
    }
}
