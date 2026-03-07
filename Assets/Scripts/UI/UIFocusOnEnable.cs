namespace UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UIFocusOnEnable : MonoBehaviour
    {
        [Header("Botão inicial para foco")]
        public Button buttonToFocus;

        void OnEnable()
        {
            if (EventSystem.current == null)
            {
                Debug.LogWarning("Não há EventSystem na cena!");
                return;
            }

            StartCoroutine(SetButtonFocus());
        }

        System.Collections.IEnumerator SetButtonFocus()
        {
            yield return null; 
            yield return null;
            EventSystem.current.SetSelectedGameObject(null); 
            EventSystem.current.SetSelectedGameObject(buttonToFocus.gameObject, new BaseEventData(EventSystem.current));
        }
    }
}