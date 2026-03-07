using Settings.ScriptableObjects;

namespace Settings
{
    using UnityEngine;
    using Settings;

    public class GameSettingsManager : MonoBehaviour
    {
        public static GameSettingsManager Instance { get; private set; }

        [Header("Settings ScriptableObject")]
        public GameSettingsSO settings;

        void Awake()
        {

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (settings == null)
            {
                Debug.LogWarning("GameSettingsSO não definido no GameSettingsManager!");
                return;
            }

            if (settings.overridePlayerPrefs || !PlayerPrefs.HasKey("DirectionalAttack"))
            {
                GameSettings.DirectionalAttack = settings.DirectionalAttack;
                GameSettings.DiagonalAttack = settings.DiagonalAttack;
                GameSettings.InfiniteWaves = settings.InfiniteWaves;
            }
        }
    }
}