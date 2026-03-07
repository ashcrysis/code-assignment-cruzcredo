using System.Collections;
using Settings;
using UnityEngine.SceneManagement;
using Util;

namespace Player
{
    using UnityEngine;
    using VFX;

    namespace Player
    {
        public class PlayerStats : MonoBehaviour
        {
            [Header("Health")]
            public int maxHealth = 5;
            public int currentHealth;

            [Header("Movement")]
            public float moveSpeed = 5f;
            public float dashSpeed = 12f;
            public float dashDuration = 0.2f;

            [Header("Combat")]
            public int attackDamage = 1;
            public int comboDamage = 2;
            public int maxComboAttacks = 2;

            [Header("Timing")]
            public float attackDuration = 0.3f;
            public float comboResetTime = 0.5f;
            public int currentCombo = 0;
            public int maxCombo = 3;

            public float comboTimer;

            [Header("Knockback")]
            public float knockbackForce = 6f;
            public bool IsKnockedBack;
            public float knockbackDuration = 0.2f;

            PlayerController controller;
            FlashTexture flash;
            Animator anim;

            [Header("Invincibility")]
            public bool IsInvincible = false;
            void Awake()
            {
                controller = GetComponent<PlayerController>();
                flash = GetComponent<FlashTexture>();
                anim = GetComponent<Animator>();

                currentHealth = maxHealth;
                GameState.Paused = false;
            }

            public void TakeDamage(int damage, Vector2 sourcePosition)
            {
                if (IsInvincible) 
                    return; 

                currentHealth -= damage;
                
                FindAnyObjectByType<Portrait>().GetComponent<Animator>().Play("PortraitHurt");
                
                if (flash != null)
                    flash.Flash();

                ApplyKnockback(sourcePosition);

                if (currentHealth <= 0)
                    Die();
            }

            void ApplyKnockback(Vector2 sourcePosition)
            {
                Vector2 direction = ((Vector2)transform.position - sourcePosition).normalized;

                controller.Rb.linearVelocity = Vector2.zero;
                controller.Rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

                StartCoroutine(KnockbackRoutine());
            }
            IEnumerator KnockbackRoutine()
            {
                IsKnockedBack = true;

                yield return new WaitForSeconds(knockbackDuration);

                controller.Rb.linearVelocity = Vector2.zero;
                IsKnockedBack = false;
            }
            IEnumerator ResetKnockback()
            {
                yield return new WaitForSeconds(knockbackDuration);

                controller.Rb.linearVelocity = Vector2.zero;
            }
            void Die()
            {
                controller.enabled = false;
                anim.Play("Death");
            }

            public void DeathEvent()
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}