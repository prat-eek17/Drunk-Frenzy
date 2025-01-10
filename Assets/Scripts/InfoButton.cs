using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour
{
    public GameObject infoPage; // Reference to the player object

    public void SetA()
    {
        infoPage.SetActive(true);
    }

    public void SetB()
    {
        infoPage.SetActive(false);
    }
}
