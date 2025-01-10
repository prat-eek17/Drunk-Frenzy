using UnityEngine;

public class basketleft : MonoBehaviour
{
    public float maxHeightChange = 3f; // Maximum height change after being touched

    private Vector3 initialPosition;
    private bool hasCollided = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasCollided && collision.CompareTag("Player"))
        {
            hasCollided = true;
            ChangeObjectHeight();
        }
    }
    void ChangeObjectHeight()
    {
        float randomHeightChange = Random.Range(-maxHeightChange, maxHeightChange);
        Vector3 targetPosition = initialPosition + new Vector3(0f, randomHeightChange, 0f);
        transform.position = targetPosition;

        hasCollided = false; // Reset the flag to allow for the next collision
    }
}
