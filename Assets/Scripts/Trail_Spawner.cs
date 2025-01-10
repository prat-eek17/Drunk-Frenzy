using Unity.VisualScripting;
using UnityEngine;

public class Trail_Spawner : MonoBehaviour
{
    private float oldPosY;
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public GameObject[] echo;

    void Start()
    {
        oldPosY = transform.position.y;
    }

    void Update()
    {
        float currentPosY = transform.position.y;
        float logic_cal = currentPosY-oldPosY;
        
        if (logic_cal<0){
            logic_cal = logic_cal*(-1);
        }
        
        //Debug.Log("Distance" + logic_cal);

        if (logic_cal > 0.1f)
        {
            SpawnTrail();
        }

        oldPosY = currentPosY;
    }

        private void SpawnTrail()
    {
        if (timeBtwSpawns <= 0)
        {
            int rand = Random.Range(0, echo.Length);
            Vector3 randomRotation = new Vector3(0f, 0f, Random.Range(0f, 360f));
            GameObject instance = Instantiate(echo[rand], transform.position, Quaternion.Euler(randomRotation));
            Destroy(instance, 0.2f);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}