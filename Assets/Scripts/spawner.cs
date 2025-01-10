using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The object prefab to spawn
    public float spawnDelay = 2f; // Time interval between spawns
    public float spawnHeightRange = 15f; // Range of heights (+5 to -5)
    
    private bool canSpawn = true;

    private void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        if (canSpawn)
        {
            float randomHeight = Random.Range(-spawnHeightRange, spawnHeightRange);
            Vector3 spawnPosition = transform.position + new Vector3(0f, randomHeight, 0f);
        
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            canSpawn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            Destroy(collision.gameObject); // Destroy the player object
            Invoke("EnableSpawning", spawnDelay); // Enable spawning after the delay
        }
    }

    void EnableSpawning()
    {
        canSpawn = true;
        SpawnObject();
    }
}
