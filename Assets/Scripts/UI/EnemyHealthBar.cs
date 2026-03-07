namespace UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Enemy;

    public class EnemyHealthBar : MonoBehaviour
    {
        public RatEnemy enemy;
        public Image healthFillImage;
        public float smoothSpeed = 5f;

        private float currentFill;

        void Start()
        {
            if (enemy != null)
                currentFill = (float)enemy.health / enemy.maxHealth;
        }

        void Update()
        {
            if (enemy == null || healthFillImage == null) return;

            float targetFill = (float)enemy.health / enemy.maxHealth;
            currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * smoothSpeed);
            healthFillImage.fillAmount = Mathf.Clamp01(currentFill);

            if (currentFill > 0.5f)
                healthFillImage.color = Color.Lerp(Color.yellow, Color.green, (currentFill - 0.5f) * 2f);
            else
                healthFillImage.color = Color.Lerp(Color.red, Color.yellow, currentFill * 2f);
        }
    }
}