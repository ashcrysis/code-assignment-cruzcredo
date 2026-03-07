using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class LoadSceneButton : MonoBehaviour
    {
        public string targetSceneName;

        public void LoadGameScene()
        {
            GameState.Paused = false;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}