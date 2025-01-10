using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScript : MonoBehaviour
{
    public GameObject GameUI;
    public Text pointText;
    public Text highScoreText;
    public Text coinsText;
    public AudioSource coinsSound;
    public AudioSource noRecord;
    public AudioSource newRecord;
    InterstitialAdsScript methodRunScript;
    movement movement;
    public GameObject basketBall;

    private void Start()
    {
        coins();
        // Load the high score from PlayerPrefs, defaulting to 0 if the key doesn't exist
        int highScore = PlayerPrefs.GetInt("high_score", 0);
        int newScore = PlayerPrefs.GetInt("current_score", 0);
        movement = basketBall.GetComponent<movement>();
        movement.touchTapFalse();

        methodRunScript = basketBall.GetComponent<InterstitialAdsScript>();
        if (methodRunScript != null) 
        {
            methodRunScript.ShowInterstitialAd();
        }
        else
        {
            Debug.LogError("InterstitialAdsScript not found on the object with tag 'Player'");
        }
    }
    // Method to deactivate the basketball
    private void DeactivateBasketBall()
    {
        basketBall.SetActive(false);
    }
    public void coins()
    {
        int newScore = PlayerPrefs.GetInt("current_score");
        int coinsEarned = Mathf.Max(0, newScore / 50); // Ensure coinsEarned is non-negative

        if (coinsEarned > 0 && coinsSound != null)
        {
            coinsSound.Play();
        }

        // Load the previous total coins from PlayerPrefs, defaulting to 0 if the key doesn't exist
        int totalCoins = PlayerPrefs.GetInt("Total_coins", 0);

        // Add the new coins earned to the previous total
        totalCoins += coinsEarned;

        // Save the updated total coins to PlayerPrefs
        PlayerPrefs.SetInt("Total_coins", totalCoins);
        PlayerPrefs.Save();
        coinsText.text = "X " + totalCoins.ToString();
    }

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointText.text = "Score: " + score.ToString();
        GameUI.SetActive(false);

        // Save the current score using PlayerPrefs
        PlayerPrefs.SetInt("current_score", score);
        PlayerPrefs.Save(); // Save the data immediately to ensure it's stored
        
        // Retrieve the saved score
        int newScore = PlayerPrefs.GetInt("current_score");

        // Retrieve the high score from PlayerPrefs
        int highScore = PlayerPrefs.GetInt("high_score", 0);

        // Compare current score with high score and update if current score is higher
        if (newScore > highScore)
        {
            newRecord.Play();
            highScore = newScore;
            PlayerPrefs.SetInt("high_score", highScore);
            PlayerPrefs.Save();
            highScoreText.text = "Best: " + highScore.ToString();
        }
        else
        {
            noRecord.Play();
            highScoreText.text = "Best: " + highScore.ToString();
        }
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("Main");
        
    }

    public void ExitBtn()
    {
        SceneManager.LoadScene("Main");
    }
}
