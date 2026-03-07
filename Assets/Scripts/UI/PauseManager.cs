using Settings;

namespace UI
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PauseManager : MonoBehaviour
    {
        [Header("UI")]
        public GameObject pauseCanvas;

        bool isPaused;

        PlayerInput input;

        void Awake()
        {
            input = FindAnyObjectByType<PlayerInput>();;
        }

        void OnEnable()
        {
            input.actions["Pause"].performed += OnPause;
        }

        void OnDisable()
        {
            input.actions["Pause"].performed -= OnPause;
        }

        void OnPause(InputAction.CallbackContext ctx)
        {
            TogglePause();
        }

        public void TogglePause()
        {
            isPaused = !isPaused;
            
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
            
            Cursor.visible = isPaused;
            GameState.Paused = isPaused;
            pauseCanvas.SetActive(isPaused);

            Time.timeScale = isPaused ? 0f : 1f;
        }

        public void ResumeGame()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            isPaused = false;
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}