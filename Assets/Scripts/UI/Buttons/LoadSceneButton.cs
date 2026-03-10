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
            Time.timeScale = 1f;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}