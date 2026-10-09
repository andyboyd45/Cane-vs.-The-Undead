using UnityEngine;
using TMPro;
public class CoinManager : MonoBehaviour
{
    private TMP_Text coinText;
    public int coinCount = 500;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Gets reference to text that displays the coin count
    void Awake()
    {
        coinText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    //Updates the coin count display
    void Update()
    {
        coinText.text = "Coins: " + coinCount.ToString();
    }
}
