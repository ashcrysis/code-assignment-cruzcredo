using UnityEngine;

namespace Player.States
{
    public class DashState : PlayerState
    {
        float dashTime = 0.2f;
        float timer;
        Vector2 dashDir;

        public DashState(PlayerController player, PlayerStateMachine sm)
            : base(player, sm) {}

        public override void Enter()
        {
            timer = dashTime;
            dashDir = player.LastMoveDirection;
            player.Stats.IsInvincible = true;
        }

        public override void Update()
        {
            timer -= Time.deltaTime;
            
            player.anim.SetBool(stateName, true);
            

            player.Rb.linearVelocity = dashDir * player.Stats.dashSpeed;

            if (timer <= 0)
            {
                player.Stats.IsInvincible = false;
                player.Rb.linearVelocity =  Vector2.zero;
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}