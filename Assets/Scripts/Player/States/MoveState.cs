using Player;
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

            Flip();

            if (player.Input.MoveInput == Vector2.zero)
                stateMachine.ChangeState(player.IdleState);

            if (player.Input.AttackPressed)
                stateMachine.ChangeState(player.CombatState);

            if (player.Input.DashPressed)
                stateMachine.ChangeState(player.DashState);
        }

        void Flip()
        {
            float x = player.Input.MoveInput.x;

            if (x == 0) return;

            if (x != 0)
                player.sr.flipX = x < 0;
        }
    }
}