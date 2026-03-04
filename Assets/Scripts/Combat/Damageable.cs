namespace Combat
{
    using UnityEngine;

    public class Damageable : MonoBehaviour
    {
        public int Health = 5;

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
                Die();
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}