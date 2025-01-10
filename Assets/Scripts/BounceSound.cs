using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSound : MonoBehaviour
{
    public GameObject player;
    public AudioSource bounceSound;
    private bool checker = true;
    void Update()
    {

        // Check if the player's height is less than 10
        if (player.transform.position.y < -5.8f && checker == true)
        {
            bounceSound.Play();
            checker = false;
        }
        if(player.transform.position.y >-5.6f && checker == false)
        {
            checker=true;
        }
    }
}
