using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Sprite[] buttonSprites; // Array of different sprites you want to assign to buttons
    public GameObject player; // Reference to the player object
    public Button[] uiButtons; // Array of UI buttons
    public Text coinsText;
    public GameObject home;
    public GameObject shop;
    int totalcoins;
    private int coins; // Player's coins
    private int skin_no; // Selected skin number loaded from PlayerPrefs
    public Sprite fixedOverlappingSprite; // The fixed overlapping sprite
    private GameObject overlappingSprite; // Reference to the overlapping sprite GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Call coinsValue() after assigning a value to coinsText
        coinsText = GetComponentInChildren<Text>(); // or assign coinsText using Unity 
        if (coinsText != null)
        {
            coinsValue();
        }
        else
        {
            Debug.LogError("coinsText is null. Make sure it's assigned in the Inspector or within the code.");
        }
        totalcoins = PlayerPrefs.GetInt("Total_coins");
        coins = totalcoins;
        coinsValue();
        // Load skin_no from PlayerPrefs
        skin_no = PlayerPrefs.GetInt("skin_no", 0);

        // Set spriteToSpawn based on the loaded skin_no
        if (skin_no >= 0 && skin_no < buttonSprites.Length)
        {
            SetPlayerSprite(buttonSprites[skin_no]);

            // Instantiate the overlapping sprite on top of the clicked button based on skin_no
            RectTransform buttonTransform = uiButtons[skin_no].GetComponent<RectTransform>();
            overlappingSprite = new GameObject("OverlappingSprite");
            RectTransform overlappingTransform = overlappingSprite.AddComponent<RectTransform>();
            overlappingTransform.SetParent(uiButtons[skin_no].transform.parent, false);
            overlappingTransform.position = buttonTransform.position;
            overlappingTransform.sizeDelta = buttonTransform.sizeDelta;
            Image image = overlappingSprite.AddComponent<Image>();
            image.sprite = fixedOverlappingSprite; // Set the fixed overlapping sprite here
        }
        else
        {
            Debug.LogError("Invalid skin number loaded from PlayerPrefs.");
        }

        // Get a reference to the player object using its tag or other methods
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Method to move the player to a new position
    public void MovePlayerOffScreen()
    {
        Vector3 newPosition = new Vector3(-10, -10);
        player.transform.position = newPosition;
    }

    public void MovePlayerBack()
    {
        Vector3 newPosition = new Vector3(0, 1.68f);
        player.transform.position = newPosition;
    }
    // Method to handle button click
    void OnButtonClick(int buttonIndex)
    {
        // Update player sprite based on the selected button
        if (buttonIndex >= 0 && buttonIndex < buttonSprites.Length)
        {
            SetPlayerSprite(buttonSprites[buttonIndex]);
            // Save the selected skin number to PlayerPrefs
            PlayerPrefs.SetInt("skin_no", buttonIndex);
            PlayerPrefs.Save();

            // Destroy existing overlapping sprite if it exists
            if (overlappingSprite != null)
            {
                Destroy(overlappingSprite);
            }

            // Instantiate the overlapping sprite on top of the clicked button
            RectTransform buttonTransform = uiButtons[buttonIndex].GetComponent<RectTransform>();
            overlappingSprite = new GameObject("OverlappingSprite");
            RectTransform overlappingTransform = overlappingSprite.AddComponent<RectTransform>();
            overlappingTransform.SetParent(uiButtons[buttonIndex].transform.parent, false);
            overlappingTransform.position = buttonTransform.position;
            overlappingTransform.sizeDelta = buttonTransform.sizeDelta;
            Image image = overlappingSprite.AddComponent<Image>();
            image.sprite = fixedOverlappingSprite; // Set the fixed overlapping sprite here
            //fix the overlapping issue
        }
        else
        {
            Debug.LogError("Invalid button index or buttonSprites array is not set up correctly.");
        }
    }

    // Method to set the player's sprite
    void SetPlayerSprite(Sprite sprite)
    {
        if (player != null)
        {
            SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
            if (playerSpriteRenderer != null)
            {
                playerSpriteRenderer.sprite = sprite;
            }
            else
            {
                Debug.LogError("Player object does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError("Player object is null.");
        }
    }
    public void coinsValue()
    {
        int totalcoins = PlayerPrefs.GetInt("Total_coins");
        coinsText.text = "X " + totalcoins.ToString();
    }
    
    public void homeScreen()
    {
        home.SetActive(true);
        player.SetActive(true);
        shop.SetActive(false);
    }

    void Update()
    {
        coinsValue();
        
        // Add listeners to the UI buttons and adjust brightness based on available coins
        for (int i = 0; i < uiButtons.Length; i++)
        {
            int buttonIndex = i; // Store the current index to avoid closure-related issues

            // Check if player has enough coins to purchase the button
            if (buttonIndex * 10 + 10 <= coins)
            {
                // Enable the button
                uiButtons[i].interactable = true;

                // Set button sprite based on buttonSprites array
                if (buttonIndex < buttonSprites.Length)
                {
                    uiButtons[i].image.sprite = buttonSprites[buttonIndex];
                }
                else
                {
                    Debug.LogError("Button sprite index out of range.");
                }

                // Set brighter color for enabled buttons
                ColorBlock colors = uiButtons[i].colors;
                colors.normalColor = Color.white; // 100% brightness
                uiButtons[i].colors = colors;

                // Add listener with a lambda expression to handle button click
                uiButtons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
            }
            else
            {
                // Disable the button if the player doesn't have enough coins
                uiButtons[i].interactable = false;

                // Set dimmer color for disabled buttons
                ColorBlock colors = uiButtons[i].colors;
                colors.normalColor = new Color(1f, 1f, 1f, 0.4f); // 40% brightness
                uiButtons[i].colors = colors;
            }
        }
    }
}
