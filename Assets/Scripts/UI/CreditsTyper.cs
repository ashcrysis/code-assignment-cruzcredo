namespace UI
{
    using System.Collections;
    using TMPro;
    using UnityEngine;

    public class CreditsTyper : MonoBehaviour
    {
        public TMP_Text textUI;

        [TextArea] public string fixedStart;
        [TextArea] public string typingText;

        public float charDelay = 0.04f;
        public float punctuationDelay = 0.25f;

        void Start()
        {
            StartCoroutine(TypeText());
        }

        IEnumerator TypeText()
        {
            string fullText = fixedStart + "\n\n" + typingText;

            textUI.text = fullText;
            textUI.maxVisibleCharacters = 0;

            for (int i = 0; i < fullText.Length; i++)
            {
                textUI.maxVisibleCharacters = i + 1;

                char c = fullText[i];

                if (c == '!' || c == '?' || c == ',' || c == '.' || c == ':')
                    yield return new WaitForSeconds(punctuationDelay);
                else
                    yield return new WaitForSeconds(charDelay);
            }
        }
    }
}