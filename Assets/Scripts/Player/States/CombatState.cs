using Player;
using Player.States;
using UnityEngine;

namespace Player.States
{
    public class CombatState : PlayerState
    {
        float timer;

        public CombatState(PlayerController player, PlayerStateMachine sm)
            : base(player, sm) {}

        public override void Enter()
        {
            timer = player.Stats.attackDuration;
            player.Stop();
            player.AttackHitbox.SetActive(true);
            
            player.anim.SetBool(stateName, true);
        }

        public override void Update()
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                player.AttackHitbox.SetActive(false);
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}