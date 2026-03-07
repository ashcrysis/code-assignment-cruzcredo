namespace UI.Buttons
{
    using UnityEngine;
    using UnityEngine.UI;
    using Settings;

    namespace UI
    {
        public class DiagonalAttackToggle : MonoBehaviour
        {
            Toggle toggle;

            void Awake()
            {
                toggle = GetComponent<Toggle>();
            }

            void Start()
            {
                toggle.isOn = GameSettings.DiagonalAttack;
            }

            public void SetDiagonalAttack(bool value)
            {
                GameSettings.DiagonalAttack = value;
            }
        }
    }
}