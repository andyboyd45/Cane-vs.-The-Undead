using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private GameObject Game_Over_Panel;
    [SerializeField] private GameObject HUD_Panel;
    [SerializeField] private GameObject Pause_Panel;
    
    [Header("Audio Clip")]
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioSource audioSource;
    private Button resetButton;
    private Button MainMenuButton;
    private Button Resume;
    private Button Exit;
    private GameObject EndTrigger;
    private int pass;
    private bool isPaused = false;

    //Grabbing buttons and other Game elements like endTrigger
    void Awake()
    {
        Time.timeScale = 1f; // Ensure the game is running at normal speed when the canvas manager awakens
        resetButton = Game_Over_Panel.transform.Find("restart").GetComponent<Button>();
        MainMenuButton = Game_Over_Panel.transform.Find("main_menu").GetComponent<Button>();
        Resume = Pause_Panel.transform.Find("ResumeButton").GetComponent<Button>();
        Exit = Pause_Panel.transform.Find("ExitButton").GetComponent<Button>();

        resetButton.onClick.AddListener(OnResetButtonClicked);
        MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        Resume.onClick.AddListener(OnResumeButtonClicked);
        Exit.onClick.AddListener(OnExitButtonClicked);

        EndTrigger = GameObject.Find("EndTrigger");
    }
    //Sets the UI panels of pause and Game Over to false
    //This hides them from the scene and makes them unavailable
    void Start()
    {
        Game_Over_Panel.SetActive(false);
        Pause_Panel.SetActive(false);
    }

    // Update is called once per frame
    //Checks if the game should be over or paused based on input or the PassOver value
    void Update()
    {
        pass = EndTrigger.GetComponent<EndEvents>().PassOver;

        if (pass <= 0)
        {
            ShowGameOverPanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Pause_Panel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f; // Pause or resume the game based on isPaused
        }
    }

    //Shows Game Over panel
    public void ShowGameOverPanel()
    {
        Time.timeScale = 0f; 
        Game_Over_Panel.SetActive(true);
        HUD_Panel.SetActive(false);
    }
    // Resets the scene
    void OnResetButtonClicked()
    {
        playButtonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Returns to main menu from game over
    void OnMainMenuButtonClicked()
    {
        playButtonSound();
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("Start_Scene");
    }

    //Resumes the game from pause
    void OnResumeButtonClicked()
    {
        playButtonSound();
        Pause_Panel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    //Exits to the main menu from pause
    void OnExitButtonClicked()
    {
        playButtonSound();
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("Start_Scene");
    }
    void playButtonSound()
    {
        audioSource.PlayOneShot(buttonClip);
    }
}
