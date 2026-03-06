using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class LoadSceneButton : MonoBehaviour
    {
        public string targetSceneName;

        public void LoadGameScene()
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}