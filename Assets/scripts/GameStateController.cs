using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Needed for Coroutines

public class GameStateController : MonoBehaviour
{
    public static GameStateController instance;

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameplayHUD;
    public GameObject winPanel;
    public GameObject nameEntry;
    public TMPro.TextMeshProUGUI finalTimeDisplay;

    [Header("Settings & Controls")]
    public GameObject controlsPanel;
    public TMPro.TextMeshProUGUI teleportKeyText;
    public UnityEngine.UI.Slider sensitivitySlider;

    private KeyCode teleportKey;
    private bool isRebinding = false;

    private bool isPaused = false;
    private bool isGameOver = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        // Default to E if no key is saved
        string savedKey = PlayerPrefs.GetString("TeleportKey", "E");

        // Safety check: try to parse the saved string back to a KeyCode
        if (System.Enum.TryParse(savedKey, out KeyCode result))
        {
            teleportKey = result;
        }
        else
        {
            teleportKey = KeyCode.E;
        }

        teleportKeyText.text = teleportKey.ToString();

        float savedSens = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        sensitivitySlider.value = savedSens;

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

    private void SetMouseState(bool visible)
    {
        if (visible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // --- BUTTON FUNCTIONS ---

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gameplayHUD.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        SetMouseState(false);
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        SetMouseState(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        SetMouseState(false);
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        gameplayHUD.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        SetMouseState(true);
    }

    public void ReturnToMenu()
    {
        // Optional: Ensure time is reset before reloading scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ShowMainMenu()
    {
        Time.timeScale = 0f;

        // Explicitly set what should be ON
        mainMenuPanel.SetActive(true);

        // Explicitly set what should be OFF
        controlsPanel.SetActive(false); // <--- Add this line
        gameplayHUD.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        if (winPanel != null) winPanel.SetActive(false);

        SetMouseState(true);
    }

    public void TriggerWin(float timeRemaining)
    {
        float timeTaken = 300f - timeRemaining;
        int minutes = Mathf.FloorToInt(timeTaken / 60);
        int seconds = Mathf.FloorToInt(timeTaken % 60);
        int milliSeconds = Mathf.FloorToInt((timeTaken % 1) * 100);

        finalTimeDisplay.text = string.Format("Time Taken: {0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);

        winPanel.SetActive(true);
        gameplayHUD.SetActive(false);
        Time.timeScale = 0f;
        SetMouseState(true);
    }

    public void EnterName(){
        // save name as public static variable
    }

    // --- REBINDING LOGIC ---

    private void OnGUI()
    {
        if (isRebinding)
        {
            Event e = Event.current;

            // Check for Keyboard Key
            if (e.isKey && e.keyCode != KeyCode.None)
            {
                ApplyNewKey(e.keyCode);
            }

            // Check for Mouse Button (Allows Left Click, Right Click, etc)
            if (e.isMouse && e.type == EventType.MouseDown)
            {
                KeyCode mouseKey = (KeyCode)((int)KeyCode.Mouse0 + e.button);
                ApplyNewKey(mouseKey);
            }
        }
    }

    private void ApplyNewKey(KeyCode newKey)
    {
        teleportKey = newKey;
        PlayerPrefs.SetString("TeleportKey", teleportKey.ToString());
        teleportKeyText.text = teleportKey.ToString();
        isRebinding = false;
    }

    public void OpenControls()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void StartRebinding()
    {
        // Use a coroutine to prevent the click used to press the button
        // from being immediately registered as the new bind.
        StartCoroutine(RebindRoutine());
    }

    private IEnumerator RebindRoutine()
    {
        teleportKeyText.text = "...";
        yield return new WaitForSecondsRealtime(0.1f); // Wait briefly
        teleportKeyText.text = "Press Any Key/Mouse";
        isRebinding = true;
    }

    public void OnSensitivityChanged()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivitySlider.value);
    }

    public KeyCode GetTeleportKey() => teleportKey;
}