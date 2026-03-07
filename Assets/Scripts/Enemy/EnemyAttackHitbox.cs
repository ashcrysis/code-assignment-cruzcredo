using Player;
using Player.Player;

namespace Enemy
{
    using UnityEngine;

    public class EnemyAttackHitbox : MonoBehaviour
    {
        public int damage = 1;
        public float knockbackForce = 6f;
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerHitbox"))
            {
                PlayerStats stats = other.GetComponentInParent<PlayerStats>();

                if (stats != null)
                {
                    stats.TakeDamage(damage, transform.position);
                }
            }
        }
    }
}