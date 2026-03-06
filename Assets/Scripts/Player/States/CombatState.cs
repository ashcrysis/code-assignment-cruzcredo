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

            player.Stop();
            
            player.AttackHitbox.SetActive(false);
            player.Stats.currentCombo++;
            player.anim.SetBool(stateName, true);

            if (player.Stats.currentCombo > player.Stats.maxCombo)
                player.Stats.currentCombo = 1;

            player.anim.SetInteger("Combo", player.Stats.currentCombo);

            timer = player.Stats.attackDuration;

            player.AttackHitbox.SetActive(true);

            player.Stats.comboTimer = player.Stats.comboResetTime;
        }
        public override void Update()
        {
            timer -= Time.deltaTime;
            player.Stats.comboTimer -= Time.deltaTime;

            if (player.Input.AttackPressed && player.Stats.comboTimer > 0)
            {
                stateMachine.ChangeState(player.CombatState);
                return;
            }

            if (timer <= 0)
            {
                player.AttackHitbox.SetActive(false);

                if (player.Stats.comboTimer <= 0)
                    player.Stats.currentCombo = 0;

                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}