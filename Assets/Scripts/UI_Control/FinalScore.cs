using UnityEngine;
using TMPro;
public class FinalScore : MonoBehaviour
{
    private TMP_Text scoretext;
    private int score = 0; //Used by ScoreCounter to track the current score
    private GameObject scoreCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Finds the game object score
    void Awake()
    {
        scoreCounter = GameObject.Find("Score");        
    }
    //Gets text compontents
    void Start()
    {
        scoretext = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    //Updates the final score display with the current score
    //This is only shown at the game over screen
    void Update()
    {
        score = scoreCounter.GetComponent<ScoreCounter>().score;
        scoretext.text = "Final Score: " + score;

    } 
}

