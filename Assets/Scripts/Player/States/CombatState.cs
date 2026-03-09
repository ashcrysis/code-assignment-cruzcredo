using System.Collections;
using UnityEngine;

namespace Player.States
{
    public class CombatState : PlayerState
    {
        bool comboWindowOpen = false;
        bool wantsNextAttack = false;

        public CombatState(PlayerController player, PlayerStateMachine sm)
            : base(player, sm) {}

        public override void Enter()
        {
            player.Stop();

            player.Stats.currentCombo++;

            if (player.Stats.currentCombo > player.Stats.maxCombo)
                player.Stats.currentCombo = 1;

            player.anim.SetInteger("Combo", player.Stats.currentCombo);
            player.anim.SetBool(stateName, true);

            player.UpdateAttackDirection();

            wantsNextAttack = false;
            comboWindowOpen = false;
        }

        public override void Update()
        {
            if (comboWindowOpen && player.Input.AttackPressed)
            {
                wantsNextAttack = true;
            }

            AnimatorStateInfo state = player.anim.GetCurrentAnimatorStateInfo(0);

            if (!comboWindowOpen && state.normalizedTime >= 1f)
            {
                player.Stats.currentCombo = 0;
                stateMachine.ChangeState(player.IdleState);
            }
        }

        public void OpenComboWindow()
        {
            comboWindowOpen = true;
            player.StartCoroutine(ComboWindowRoutine());
        }

        IEnumerator ComboWindowRoutine()
        {
            yield return new WaitForSeconds(player.Stats.comboWindowTime);

            comboWindowOpen = false;

            if (wantsNextAttack)
            {
                stateMachine.ChangeState(player.CombatState);
            }
            else
            {
                player.Stats.currentCombo = 0;
                stateMachine.ChangeState(player.IdleState);
            }
        }

        public override void Exit()
        {
            player.anim.SetBool(stateName, false);
        }
    }
}