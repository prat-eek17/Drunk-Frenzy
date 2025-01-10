using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource scoreSound;
    public AudioSource scoreBoosterSound;
    public AudioSource fastMusic;
    public AudioSource booing;
    public GameOverScript gameOverScreen;
    public GameObject ball;
    public GameObject righthoop;
    public GameObject lefthoop;
    public Text scoretext;
    public int score = 0;
    public Scrollbar timerScrollbar;
    public Text messageText;
    private string[] randomMessages = {"Amazing!","Impressive!","Great!","Super!","Cool!","Awesome!","Sweet!"};
    private int fasttimer = 10;
    private int hoopdirection = -1; // left = -1 and right = 1
    private bool isGameOver = false;
    private int actualscore = 1;
    private int ScoreAdd = 0;
    private float timerProgress;
    public GameObject BlockControl;
    //for colour change
    public Image backgroundImage; // Reference to the background image component
    private Color defaultColor = Color.white; // Default color of the background image
    private Color[] changingColors = {Color.red, Color.yellow, Color.green, Color.blue, Color.magenta, Color.cyan, Color.gray}; // Colors to cycle through
    private bool isChangingColor = false; // Flag to check if changing color is in progress

    void Start()
    {
        messageText.gameObject.SetActive(false);
        lefthoop.SetActive(true);
        righthoop.SetActive(false);
    }

    public void playerscored()
    {
        scoreSound.Play();
        score++;
        score = score + ScoreAdd;
        scoretext.text = score.ToString();
        actualscore++;
        ResetTimer();
        ChangeDirection();
        scorebooster();
    }

    void ChangeDirection()
    {
        hoopdirection *= -1;

        if (hoopdirection == 1)
        {
            lefthoop.SetActive(false);
            righthoop.SetActive(true);
        }
        else if (hoopdirection == -1)
        {
            lefthoop.SetActive(true);
            righthoop.SetActive(false);
        }
    }

    public void scorebooster()
    {
        if (timerProgress > 0.4f && ScoreAdd < 7)
        {
            scoreBoosterSound.Play(); 

            if(!fastMusic.isPlaying){
                fastMusic.Play();
            }
            if(fastMusic.isPlaying){
                fastMusic.UnPause();
            }
           
            ScoreAdd = ScoreAdd + 2;
            isChangingColor = true;
            StartCoroutine(ChangeBackgroundColorSmoothly());
            // Calculate the midpoint position between the ball and the center of the screen
            Vector3 midpointPosition = (ball.transform.position + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0))) / 2f;
             // Set a random message from the array
            string randomMessage = randomMessages[Random.Range(0, randomMessages.Length)];
            messageText.text = randomMessage;
            // Set the position of the messageText to the calculated midpoint position
            messageText.transform.position = midpointPosition;

            StartCoroutine(ShakeCamera());
            messageText.gameObject.SetActive(true);

            // Use a smaller duration for faster growth (e.g., 0.2f)
            StartCoroutine(ShowTextWithFontSizeAnimation(0.2f, 50, 70));

            StartCoroutine(HideTextAfterDelay(0.65f));
        }
        if (timerProgress < 0.4f)
        {
            isChangingColor = false;
            StopCoroutine(ChangeBackgroundColorSmoothly());
            backgroundImage.color = defaultColor;
            ScoreAdd = 0;
            fastMusic.Pause();
        }
        if (timerProgress > 0.4f && ScoreAdd > 7)
        {
            isChangingColor = true;
            scoreBoosterSound.Play(); 
            StartCoroutine(ChangeBackgroundColorSmoothly());
            StartCoroutine(ShakeCamera());

            // Calculate the midpoint position between the ball and the center of the screen
            Vector3 midpointPosition = (ball.transform.position + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0))) / 2f;

            // Set the position of the messageText to the calculated midpoint position
            messageText.transform.position = midpointPosition;

            messageText.gameObject.SetActive(true);

            // Use a smaller duration for faster growth (e.g., 0.2f)
            StartCoroutine(ShowTextWithFontSizeAnimation(0.2f, 50, 70));

            StartCoroutine(HideTextAfterDelay(0.65f));
        }
    }

    private IEnumerator ShakeCamera()
    {
        float shakeDuration = 0.2f; // Adjust the duration of the shake
        float shakeMagnitude = 0.1f; // Adjust the intensity of the shake

        Vector3 originalCameraPosition = Camera.main.transform.position;

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            Camera.main.transform.position = new Vector3(originalCameraPosition.x + x, originalCameraPosition.y + y, originalCameraPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.position = originalCameraPosition;
    }

    private IEnumerator ShowTextWithFontSizeAnimation(float duration, int startSize, int endSize)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            int fontSize = (int)Mathf.Lerp(startSize, endSize, t);
            messageText.fontSize = fontSize;

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        messageText.fontSize = endSize;
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.gameObject.SetActive(false);
    }

    void ResetTimer()
    {
        if (score > 1)
        {
            ResetGameOverTimer();
        }
    }

    void Update()
    {
        // Update the game over timer if the score is 1
        if (score >= 1 && !isGameOver)
        {
            ChangeBackgroundColorSmoothly();
            // Update the Scrollbar value based on the timer progress
            timerProgress = Mathf.Clamp01(timerScrollbar.size - (Time.deltaTime / fasttimer));
            timerScrollbar.size = timerProgress;

            if (timerProgress <= 0)
            {
                BlockControl.SetActive(true);

                if(ball.transform.position.y < -4){
                    isGameOver = true;
                    isChangingColor = false;
                    fastMusic.Stop();
                    GameOver();
                }
            }
            if (timerProgress>0){
                BlockControl.SetActive(false);
            }
        }
    }

    void GameOver()
    {
        gameOverScreen.Setup(score);
    }
    // Reset the game over timer to its initial duration
    void ResetGameOverTimer()
    {
        timerScrollbar.size = 1f;
        isGameOver = false;

        if (actualscore % 4 == 0 && actualscore < 23)
        {
            fasttimer--;
        }
    }

    // Coroutine to smoothly change background color in a loop
    private IEnumerator ChangeBackgroundColorSmoothly()
    {
        if (isChangingColor)
        {
            int colorIndex = 0;

            while (isChangingColor)
            {
                Color targetColor = changingColors[colorIndex];

                // Desaturate the target color
                float gray = (targetColor.r + targetColor.g + targetColor.b) / 3f;
                targetColor = new Color(
                    Mathf.Lerp(targetColor.r, gray, 0.5f),
                    Mathf.Lerp(targetColor.g, gray, 0.5f),
                    Mathf.Lerp(targetColor.b, gray, 0.5f)
                );

                float duration = 1.0f; // Change duration as needed
                float elapsedTime = 0f;
                Color startColor = backgroundImage.color;

                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    backgroundImage.color = Color.Lerp(startColor, targetColor, t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                backgroundImage.color = targetColor;

                yield return new WaitForSeconds(0.8f); // Adjust delay between color changes

                colorIndex = (colorIndex + 1) % changingColors.Length;

                // Check if the condition to stop changing color is met
                if (timerProgress < 0.4f && ScoreAdd < 7)
                {
                    isChangingColor = false;
                    backgroundImage.color = Color.white;
                    fastMusic.Stop();
                    booing.Play();
                }
            }
        }
    }
}