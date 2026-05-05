using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static GameStateController instance;

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameplayHUD;

    private bool isPaused = false;
    private bool isGameOver = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        ShowMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver && !mainMenuPanel.activeSelf)
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    // --- HELPER FOR MOUSE ---
    private void SetMouseState(bool visible)
    {
        if (visible)
        {
            Cursor.lockState = CursorLockMode.None; // Unlocks the mouse
            Cursor.visible = true;                  // Shows the mouse
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Locks mouse to center
            Cursor.visible = false;                   // Hides the mouse
        }
    }

    // --- BUTTON FUNCTIONS ---

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gameplayHUD.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;

        SetMouseState(false); // Hide mouse for gameplay
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        SetMouseState(true); // Show mouse to click buttons
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        SetMouseState(false); // Hide mouse when returning to game
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        gameplayHUD.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        SetMouseState(true); // Show mouse for Game Over menu
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ShowMainMenu()
    {
        Time.timeScale = 0f;
        mainMenuPanel.SetActive(true);
        gameplayHUD.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        SetMouseState(true); // Show mouse for Main Menu
    }
}