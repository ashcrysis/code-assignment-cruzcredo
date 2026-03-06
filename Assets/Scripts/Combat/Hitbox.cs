using Player.Player;

namespace Combat
{
    using UnityEngine;

    public class Hitbox : MonoBehaviour
    {
        public PlayerStats Stats;

        void Awake()
        {
            Stats = GetComponentInParent<PlayerStats>();
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            Damageable damageable = other.GetComponent<Damageable>();

            if (damageable != null)
            {
                int damage;

                if (Stats.currentCombo == Stats.maxCombo)
                    damage = Stats.comboDamage;
                else
                    damage = Stats.attackDamage;

                damageable.TakeDamage(damage);
            }
        }
    }
}