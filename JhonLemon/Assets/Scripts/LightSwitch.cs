using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightSwitch : MonoBehaviour
{
    private bool PlayerInZone;

    public GameObject lightorobj;
    public GameObject lightorobj2;

    public GameObject dialog;
    public GameObject Ghost;

    public bool activeGhost = false;

    public TextMeshProUGUI textLight;
    public Light lightGlobal;
    // Start is called before the first frame update

    public bool active = true;
    int var = 0;
    void Start()
    {
        PlayerInZone = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.F))
        {
            lightorobj.SetActive(true);
            lightorobj2.SetActive(true);
            active = false;
            lightGlobal.intensity = 0.5f;
            if(activeGhost)
            {
                Ghost.SetActive(true);
            }
            
            var = 1;
            textLight.text = ("1/1");
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = true;
        }
        if(var == 0)
        {
            dialog.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        dialog.SetActive(false);
    }
}