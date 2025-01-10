using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonmutedornot : MonoBehaviour
{
    private bool isMuted;
    public Button muteButton;
    public Image imageToShow;
    public Image imageToHide;

    void Start()
    {
        // Load the mute state from PlayerPrefs, default to false if not found
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        // Add a listener to your button's click event
        muteButton.onClick.AddListener(ToggleMute);

        // Set the initial visibility of the images based on the mute state
        SetImageVisibility();
    }

    void Update()
    {
        // You can still use Update for other things if needed
    }

    void ToggleMute()
    {
        // Toggle the mute state
        isMuted = !isMuted;

        // Save the mute state to PlayerPrefs
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        // Set the visibility of the images based on the updated mute state
        SetImageVisibility();

        // Perform any other actions based on the mute state if needed
        if (isMuted)
        {
            Debug.Log("Audio is muted!");
        }
        else
        {
            Debug.Log("Audio is not muted!");
        }
    }

    void SetImageVisibility()
    {
        // Set the visibility of the images based on the mute state
        imageToShow.gameObject.SetActive(isMuted);
        imageToHide.gameObject.SetActive(!isMuted);
    }
}
