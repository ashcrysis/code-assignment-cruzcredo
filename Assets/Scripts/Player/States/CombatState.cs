using UnityEngine;

namespace Player.States
{
    public class CombatState : PlayerState
    {
        private float attackTimer = 0f;
        private float cooldownTimer = 0f;
        private bool wantsNextAttack = false;
        private float comboResetTimer = 0f;
        public CombatState(PlayerController player, PlayerStateMachine sm)
            : base(player, sm) {}

        public override void Enter()
        {
            player.Stop();
            
            comboResetTimer = player.Stats.comboResetTime;
            
            player.Stats.currentCombo++;
            if (player.Stats.currentCombo > player.Stats.maxCombo)
                player.Stats.currentCombo = 1;
            
            player.anim.SetInteger("Combo", player.Stats.currentCombo);
            player.anim.SetBool(stateName, true);

            player.UpdateAttackDirection();

            attackTimer = player.Stats.attackDuration;
            cooldownTimer = player.Stats.attackCooldown;
            wantsNextAttack = false;
        }
        public override void Update()
        {
            float dt = Time.deltaTime;

            attackTimer -= dt;
            cooldownTimer -= dt;

            if (player.Input.AttackPressed)
            {
                if (attackTimer <= player.Stats.comboWindowTime)
                    wantsNextAttack = true;
            }

            if (attackTimer <= 0f)
            {
                if (player.Stats.currentCombo > 0 && !wantsNextAttack)
                {
                    comboResetTimer -= dt;
                    if (comboResetTimer <= 0f)
                    {
                        player.Stats.currentCombo = 0;
                    }
                }
                else
                {
                    stateMachine.ChangeState(player.IdleState);
                }
            }
        }
        public void OnAttackAnimationEnd()
        {
            attackTimer = 0f; 
        }

        public override void Exit()
        {
            player.anim.SetBool(stateName, false);
        }
    }
}