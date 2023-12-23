using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    private LightSwitch lightSwitch;
    private PlayerMovement playerMovement;
    private void Start()
    {
        lightSwitch = FindObjectOfType<LightSwitch>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        if(playerMovement.contcomida == 1)
        {
            wall1.SetActive(false);
        }
        if (playerMovement.contcomida == 2)
        {
            wall2.SetActive(false);
        }
        if (playerMovement.contcomida == 3)
        {
            wall4.SetActive(false);
        }
        if (lightSwitch.active == false)
        {
            wall3.SetActive(false);
        }
    }
}
