namespace UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Player.Player;

    public class PlayerHealthBar : MonoBehaviour
    {
        public PlayerStats playerStats;
        public Image healthFillImage;
        public float smoothSpeed = 5f;

        private float currentFill;

        void Start()
        {
            if (playerStats != null)
                currentFill = (float)playerStats.currentHealth / playerStats.maxHealth;
        }

        void Update()
        {
            if (playerStats == null || healthFillImage == null) return;

            float targetFill = (float)playerStats.currentHealth / playerStats.maxHealth;
            currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * smoothSpeed);
            healthFillImage.fillAmount = Mathf.Clamp01(currentFill);

            if (currentFill > 0.5f)
                healthFillImage.color = Color.Lerp(Color.yellow, Color.green, (currentFill - 0.5f) * 2f);
            else
                healthFillImage.color = Color.Lerp(Color.red, Color.yellow, currentFill * 2f);
        }
    }
}