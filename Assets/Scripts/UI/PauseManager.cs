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
        Animator playerAnim;

        void Awake()
        {
            input = FindAnyObjectByType<PlayerInput>();
            playerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
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
            AnimatorStateInfo state = playerAnim.GetCurrentAnimatorStateInfo(0); 

            if (!state.IsName("Victory"))
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