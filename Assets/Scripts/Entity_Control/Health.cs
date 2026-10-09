using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int rewardCoins = 0; // Coins rewarded for killing an enemy
    private int currentHealth;
    private Animator animator;
    private CoinManager coinManager;
    private ScoreCounter scoreCounter;
    public bool isDead = false; 

    //Gets animator components
    //Gets references to the coin and score managers
    //(Coin and score is only used when enemy dies)
    void Awake()
    {
        animator = GetComponent<Animator>();

        GameObject coins = GameObject.Find("Coins");
        coinManager = coins.GetComponent<CoinManager>();

        GameObject score = GameObject.Find("Score");
        scoreCounter = score.GetComponent<ScoreCounter>();
    }

    // Start is called before the first frame update
    //Sets current health to the max heath before they spawn
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    //Checks if entity's health is 0
    //Will set death status
    void Update()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
        }
    }

    //Handles the death animation
    //Triggered by animation
    //Will give coins and score to player if entity was an enemy
    void onDeath()
    {
        if(gameObject.tag == "Enemy")
        {
            coinManager.coinCount += rewardCoins;
            scoreCounter.score += rewardCoins;
        }
        Destroy(gameObject);
    }

    //Handles entity taking damage
    public void TakeDamage(int damage){
        currentHealth -= damage;
    }
    
}
