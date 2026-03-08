namespace UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine.InputSystem;

    public class UIFocusOnEnable : MonoBehaviour
    {
        [Header("Starter Focus button")]
        public Button buttonToFocus;

        [Header("Interval to check in seconds")]
        public float checkInterval = 1f;

        private bool isChecking;

        void OnEnable()
        {
            if (EventSystem.current == null)
            {
                Debug.LogWarning("Não há EventSystem na cena!");
                return;
            }

            isChecking = true;
            StartCoroutine(CheckFocusLoop());
        }

        void OnDisable()
        {
            isChecking = false;
        }

        System.Collections.IEnumerator CheckFocusLoop()
        {
            while (isChecking)
            {
                yield return null;

                if (EventSystem.current.currentSelectedGameObject == null &&
                    Gamepad.all.Count > 0 &&
                    buttonToFocus != null &&
                    buttonToFocus.gameObject.activeInHierarchy &&
                    buttonToFocus.interactable)
                {
                    EventSystem.current.SetSelectedGameObject(
                        buttonToFocus.gameObject,
                        new BaseEventData(EventSystem.current)
                    );
                }

                yield return new WaitForSeconds(checkInterval);
            }
        }
    }
}