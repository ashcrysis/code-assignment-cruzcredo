namespace UI.Buttons
{
    using UnityEngine;
    using UnityEngine.UI;
    using Settings;

        public class InfiniteWavesToggle : MonoBehaviour
        {
            Toggle toggle;

            void Awake()
            {
                toggle = GetComponent<Toggle>();
            }

            void Start()
            {
                toggle.isOn = GameSettings.InfiniteWaves;
            }

            public void SetInfiniteWaves(bool value)
            {
                GameSettings.InfiniteWaves = value;
            }
        }
}