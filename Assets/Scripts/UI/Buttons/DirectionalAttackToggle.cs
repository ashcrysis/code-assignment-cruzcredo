using UnityEngine;
using UnityEngine.UI;
using Settings;

namespace UI
{
    public class DirectionalAttackToggle : MonoBehaviour
    {
        Toggle toggle;

        void Awake()
        {
            toggle = GetComponent<Toggle>();
        }

        void Start()
        {
            toggle.isOn = GameSettings.DirectionalAttack;
        }

        public void SetDirectionalAttack(bool value)
        {
            GameSettings.DirectionalAttack = value;
        }
    }
}