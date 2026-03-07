namespace Settings
{
    using UnityEngine;

    public static class GameSettings
    {
        const string DIRECTIONAL_ATTACK = "DirectionalAttack";
        const string INFINITE_WAVES = "InfiniteWaves";

        public static bool DirectionalAttack
        {
            get => PlayerPrefs.GetInt(DIRECTIONAL_ATTACK, 0) == 1;
            set => PlayerPrefs.SetInt(DIRECTIONAL_ATTACK, value ? 1 : 0);
        }

        public static bool InfiniteWaves
        {
            get => PlayerPrefs.GetInt(INFINITE_WAVES, 0) == 1;
            set => PlayerPrefs.SetInt(INFINITE_WAVES, value ? 1 : 0);
        }
    }
}