namespace Settings
{
    using UnityEngine;

    public class GameSettingsDefault : MonoBehaviour
    {
        [Header("Player Combat Settings")]
        public bool DirectionalAttack = false;
        public bool DiagonalAttack = false;

        [Header("Game Settings")]
        public bool InfiniteWaves = false;

        void Awake()
        {
            if (!PlayerPrefs.HasKey("DirectionalAttack"))
                GameSettings.DirectionalAttack = DirectionalAttack;

            if (!PlayerPrefs.HasKey("DiagonalAttack"))
                GameSettings.DiagonalAttack = DiagonalAttack;

            if (!PlayerPrefs.HasKey("InfiniteWaves"))
                GameSettings.InfiniteWaves = InfiniteWaves;
        }
    }
}