using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public GameObject unpauseUI;
    public void PauseGame()
    {
        unpauseUI.SetActive(true);
        Time.timeScale = 0f; // This freezes the game
    }

    public void ResumeGame()
    {
        unpauseUI.SetActive(false);
        Time.timeScale = 1f; // This resumes the game
    }
}
