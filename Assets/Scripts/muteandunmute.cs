using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muteandunmute : MonoBehaviour
{
    private bool isMuted;

    private void Update()
    {
        // Load the mute state from PlayerPrefs, default to false if not found
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        SetVolume();
    }

    public void ButtonIsPressed()
    {
        isMuted = true;
        SetVolume();
        // Save the mute state to PlayerPrefs
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ButtonIsNotPressed()
    {
        isMuted = false;
        SetVolume();
        // Save the mute state to PlayerPrefs
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void SetVolume()
    {
        AudioListener.volume = isMuted ? 0 : 1;
    }
}
