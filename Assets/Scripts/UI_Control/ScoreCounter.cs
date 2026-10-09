using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{

    public int score = 0; //Used by other scripts to update score
    private TMP_Text scoretext;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Gets text components for the score display
    void Awake()
    {
        scoretext = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    //Updates score display every frame
    void Update()
    {
        scoretext.text = "Score: " + score;
    }
}
