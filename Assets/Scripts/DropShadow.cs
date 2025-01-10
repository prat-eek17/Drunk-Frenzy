using UnityEngine;
using UnityEngine.UI;

public class DropShadow : MonoBehaviour
{
    public GameObject player;
    public GameObject dropShadow;
    private float playerX;
    private Graphic dropShadowGraphic;

    void Start()
    {
        dropShadowGraphic = dropShadow.GetComponent<Graphic>();
    }

    void Update()
    {   
        playerX = player.transform.position.x;

        // Set only the x-coordinate of the dropShadow to match the player's x-coordinate
        Vector3 shadowPosition = dropShadow.transform.position;
        shadowPosition.x = playerX;
        shadowPosition.y = -6.31f;
        dropShadow.transform.position = shadowPosition;

        // Get the distance between player and dropShadow
        float distance = Vector3.Distance(player.transform.position, dropShadow.transform.position);
        // Clamp the distance within the range [0, 100]
        distance = Mathf.Clamp(distance, 0f, 100f);
        // Map the distance to a range [0, 1]
        float normalizedDistance = Mathf.InverseLerp(0f, 100f, (distance - 90) * 100f);
        // Set the alpha (transparency) of the drop shadow material based on the normalized distance
        Color shadowColor = dropShadowGraphic.color;
        shadowColor.a = 1 - normalizedDistance; // Invert the value for better visual effect
        dropShadowGraphic.color = shadowColor;

        // Adjust the shadow size based on the normalized distance
        float shadowSize = Mathf.Lerp(1f, 0.5f, normalizedDistance);
        dropShadow.transform.localScale = new Vector3(shadowSize, shadowSize, 1f);
    }
}
