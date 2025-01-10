using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBallSkinDefault : MonoBehaviour
{
    public Sprite[] basketballSprites; // Array of basketball sprites
    private int buttonIndex; // Variable to store the PlayerPrefs value

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the buttonIndex value from PlayerPrefs
        buttonIndex = PlayerPrefs.GetInt("skin_no", 0); // 0 is the default value if "buttonIndex" is not set

        // Ensure buttonIndex is within the bounds of basketballSprites array
        if(buttonIndex >= 0 && buttonIndex < basketballSprites.Length)
        {
            // Set the sprite of the GameObject to the selected sprite from basketballSprites array
            GetComponent<SpriteRenderer>().sprite = basketballSprites[buttonIndex];
        }
        else
        {
            // Handle the case where buttonIndex is out of bounds (for example, set a default sprite)
            Debug.LogError("Invalid buttonIndex value!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
    }
}
