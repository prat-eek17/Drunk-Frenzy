using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whistle : MonoBehaviour
{
    public AudioSource whistleSound;

    // Start is called before the first frame update
    void Start()
    {
        whistleSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
