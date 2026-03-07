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

            if (player.Stats.currentCombo > player.Stats.maxCombo)
                player.Stats.currentCombo = 1;

            player.anim.SetBool(stateName, true);
            player.anim.SetInteger("Combo", player.Stats.currentCombo);

            player.UpdateAttackDirection();

            timer = player.Stats.attackDuration;

            player.AttackHitbox.SetActive(true);

            player.Stats.comboTimer = player.Stats.comboResetTime;
        }

        public override void Update()
        {
            timer -= Time.deltaTime;
            player.Stats.comboTimer -= Time.deltaTime;

            if (player.Stats.comboTimer <= 0)
            {
                player.Stats.currentCombo = 0;
                player.anim.SetInteger("Combo", 0);
            }

            if (player.Input.AttackPressed && player.Stats.comboTimer > 0)
            {
                player.AttackHitbox.SetActive(false);
                stateMachine.ChangeState(player.CombatState);
                return;
            }

            if (timer <= 0)
            {
                player.AttackHitbox.SetActive(false);
                stateMachine.ChangeState(player.IdleState);
            }
        }

        public override void Exit()
        {
            player.anim.SetBool(stateName, false);
            player.AttackHitbox.SetActive(false);
        }
    }
}