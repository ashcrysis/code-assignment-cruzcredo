namespace Settings
{
    using UnityEngine;

    public static class GameSettings
    {
        const string DIRECTIONAL_ATTACK = "DirectionalAttack";

        public static bool DirectionalAttack
        {
            get => PlayerPrefs.GetInt(DIRECTIONAL_ATTACK, 0) == 1;
            set => PlayerPrefs.SetInt(DIRECTIONAL_ATTACK, value ? 1 : 0);
        }
    }
}