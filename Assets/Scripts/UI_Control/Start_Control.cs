using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Start_Control : MonoBehaviour
{
    [Header("SFX Source")]
    [SerializeField] private AudioSource sfxSource;
    [Header("SFX Clips")]
    [SerializeField] private AudioClip buttonClip;
    private Button start;
    private Button exit;
    //Grabs buttons and creates listeners
    //This script is used for the start_scene
    void Awake()
    {
        start = gameObject.transform.Find("Start").GetComponent<Button>();
        exit = gameObject.transform.Find("Exit").GetComponent<Button>();
        start.onClick.AddListener(OnStartButtonClicked);
        exit.onClick.AddListener(OnExitButtonClicked);
    }

    //Loads the endless level scene
    private void OnStartButtonClicked()
    {
        sfxSource.PlayOneShot(buttonClip);
        SceneManager.LoadScene("Endless_level");
    }

    //Quits the game
    private void OnExitButtonClicked()
    {
        sfxSource.PlayOneShot(buttonClip);
        Application.Quit();
    }
}
