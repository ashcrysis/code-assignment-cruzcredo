using Enemy;
using Player.Player;

namespace Combat
{
    using UnityEngine;

    using System.Collections.Generic;
    using Enemy;
    using Player.Player;
    using UnityEngine;

    namespace Combat
    {
        public class Hitbox : MonoBehaviour
        {
            public PlayerStats Stats;

            HashSet<RatEnemy> enemiesHit = new HashSet<RatEnemy>();
            Vector2 knockbackDir;
            void Awake()
            {
                Stats = GetComponentInParent<PlayerStats>();
            }

            void OnEnable()
            {
                enemiesHit.Clear();
            }

            void OnTriggerEnter2D(Collider2D other)
            {
                RatEnemy enemy = other.GetComponent<RatEnemy>();
                knockbackDir = transform.right.normalized;
                if (enemy == null)
                    return;

                if (enemiesHit.Contains(enemy))
                    return;

                enemiesHit.Add(enemy);

                int damage;

                if (Stats.currentCombo == Stats.maxCombo)
                    damage = Stats.comboDamage;
                else
                    damage = Stats.attackDamage;

                enemy.TakeDamage(damage, knockbackDir);
            }
        }
    }
}