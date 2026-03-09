using Player;
using Player.Player;

namespace Enemy
{
    using UnityEngine;

    public class EnemyAttackHitbox : MonoBehaviour
    {
        public int damage = 1;

        bool hasHit;

        void OnEnable()
        {
            hasHit = false;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (hasHit)
                return;

            if (other.CompareTag("PlayerHitbox"))
            {
                PlayerStats stats = other.GetComponentInParent<PlayerStats>();

                if (stats != null)
                {
                    stats.TakeDamage(damage, transform.position);
                    hasHit = true;
                }
            }
        }
    }
}