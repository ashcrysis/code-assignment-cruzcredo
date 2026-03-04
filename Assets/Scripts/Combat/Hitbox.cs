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
                damageable.TakeDamage(Stats.attackDamage);
            }
        }
    }
}