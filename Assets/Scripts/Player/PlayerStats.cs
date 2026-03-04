namespace Player
{
    using UnityEngine;

    namespace Player
    {
        public class PlayerStats : MonoBehaviour
        {
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
        }
    }
}