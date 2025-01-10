using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class movement : MonoBehaviour
{
    public GameObject BlockTouch;
    public GameObject InGameUI;
    public GameObject MainUi;
    public GameObject shop;
    public Text coinsText;
    public float forceAmount = 10.0f; // Adjust the force amount
    private Rigidbody2D rb;
    public GameManager gameManager;
    private bool triggerIsSet = false;
    private bool isMousePressed = false;
    private bool hasScored = false;
    private bool mainUiDisabled = false;
    private bool rightorleft = false;
    public AudioSource mainMenuMusic;
    public AudioSource jumpSound;
    public bool touchTap=true;
    int blockTouchIsActive = 0;
    public void touchTapFalse()
    {
        touchTap=false;
    }
    private void Start()
    {
        Time.timeScale = 1f; // This resumes the game
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Set initial gravity to 0
        mainMenuMusic.Play();
    }
    private void Update()
    {
        BlockTouchIsActive();
        if(touchTap==true && blockTouchIsActive==0)
        {
            coinsValue();
            if (Input.GetMouseButtonDown(0) && !isMousePressed && !rightorleft && triggerIsSet)
            {
                jumpSound.Play();
                isMousePressed = true;
                ApplyForceright();
                rb.gravityScale = 2f;

                // Check if MainUi is not disabled yet
                if (!mainUiDisabled)
                {
                    MainUi.SetActive(false);
                    InGameUI.SetActive(true);
                    mainUiDisabled = true; // Set the flag to true to indicate it's disabled
                }
            }
            else if (Input.GetMouseButtonDown(0) && !isMousePressed && rightorleft && triggerIsSet)
            {
                jumpSound.Play();
                isMousePressed = true;
                ApplyForceleft();
                rb.gravityScale = 2f;

                // Check if MainUi is not disabled yet
                if (!mainUiDisabled)
                {
                    MainUi.SetActive(false);
                    InGameUI.SetActive(true);
                    mainUiDisabled = true; // Set the flag to true to indicate it's disabled
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isMousePressed = false;
            }
        }
    }

    void BlockTouchIsActive()
    {
        if (BlockTouch.activeSelf)
        {
            Debug.Log("BlockTouch");
            blockTouchIsActive = 1;
        }
        if(BlockTouch.activeSelf==(false))
        {
            Debug.Log("Allow Touch");
            blockTouchIsActive = 0;
        }
    }
    public void quitToMenu()
    {
        SceneManager.LoadScene("Main");
        blockTouchIsActive = 0;
    }
    public void triggerToStart()
    {
        jumpSound.Play();
        triggerIsSet = true;
        isMousePressed = true;
        ApplyForceright();
        rb.gravityScale = 2f;

        // Check if MainUi is not disabled yet
        if (!mainUiDisabled)
        {
            mainMenuMusic.Stop();
            MainUi.SetActive(false);
            InGameUI.SetActive(true);
            mainUiDisabled = true; // Set the flag to true to indicate it's disabled
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "right")
        {
            rightorleft = true;
        }
        if (collision.tag == "left")
        {
            rightorleft = false;
        }
        if (collision.tag == "score" && !hasScored)
        {
            hasScored = true;
            gameManager.playerscored();
            StartCoroutine(ResetScoreFlag());
        }
    }
    IEnumerator ResetScoreFlag()
    {
        yield return new WaitForSeconds(1); // Add your desired delay here
        hasScored = false;
    }

    void ApplyForceright()
    {
        Vector2 forceDirection = new Vector2(0.3f, 1f);
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        rb.AddForce(forceDirection * forceAmount, ForceMode2D.Impulse);
    }

    void ApplyForceleft()
    {
        Vector2 forceDirection = new Vector2(-0.3f, 1f);
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        rb.AddForce(forceDirection * forceAmount, ForceMode2D.Impulse);
    }
    public void enableShop()
    {
        shop.SetActive(true);
        MainUi.SetActive(false);
    }

    public void coinsValue()
    {
        int totalcoins = PlayerPrefs.GetInt("Total_coins");
        coinsText.text = "X " + totalcoins.ToString();
    }
}
