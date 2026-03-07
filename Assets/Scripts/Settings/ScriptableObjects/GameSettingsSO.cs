namespace Settings.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "GameSettingsSO", menuName = "Settings/GameSettings")]
    public class GameSettingsSO : ScriptableObject
    {
        [Header("Player Combat Settings")]
        public bool DirectionalAttack = true;
        public bool DiagonalAttack = true;

        [Header("Game Settings")]
        public bool InfiniteWaves = false;

        [Header("Overrides")]
        public bool overridePlayerPrefs = false; 
    }

}