using UnityEngine;

public class BallLocation : MonoBehaviour
{
    public GameObject player; // Reference to the player object

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player object is not assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Get the player's position
            Vector3 playerPosition = player.transform.position;

            // Set the object's position to the player's position
            transform.position = playerPosition;
        }
    }
}
