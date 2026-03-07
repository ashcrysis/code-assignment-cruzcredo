using UnityEngine;
using UnityEngine.UI;
using Settings;

namespace UI
{
    public class DirectionalAttackToggle : MonoBehaviour
    {
        Toggle toggle;

        [Header("GameObject to toggle")]
        public GameObject targetObject; 

        void Awake()
        {
            toggle = GetComponent<Toggle>();
        }

        void Start()
        {
            toggle.isOn = GameSettings.DirectionalAttack;

            if (targetObject != null)
                targetObject.SetActive(toggle.isOn);
        }

        public void SetDirectionalAttack(bool value)
        {
            GameSettings.DirectionalAttack = value;

            if (targetObject != null)
                targetObject.SetActive(value); 
        }
    }
}