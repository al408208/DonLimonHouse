using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myrotator : MonoBehaviour
{
    public GameObject player;
    float rotateSpeed = 100f;
    [SerializeField] private PlayerMovement playermov; 
    GameObject[] portions;
    int currentIndex;
    float lastChange;
    float interval = 0.12f;
    bool comido=false;
    int flag=0;


	void Start () {
        playermov=FindObjectOfType<PlayerMovement>();
        bool skipFirst = transform.childCount > 4;
        portions = new GameObject[skipFirst ? transform.childCount-1 : transform.childCount];
        for (int i = 0; i < portions.Length; i++)
        {
            portions[i] = transform.GetChild(skipFirst ? i + 1 : i).gameObject;
            if (portions[i].activeInHierarchy)
                currentIndex = i;
        }
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            comido=true;
        }
    }
	
	void Update () {
        transform.Rotate (Vector3.up * Time.deltaTime*rotateSpeed);

        //transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime*4);
        if(comido){
            if (Time.time - lastChange > interval)
            {
                Consume();
                lastChange = Time.time;
            }
            if(flag==0){
                playermov.addComida();
                flag=1;
            }
        }
        
	}

    void Consume()
    {
        if (currentIndex != portions.Length)
            portions[currentIndex].SetActive(false);
        currentIndex++;
        if (currentIndex > portions.Length){
            Destroy(gameObject);
        }

        if (currentIndex < portions.Length)
            portions[currentIndex].SetActive(true);
    }
}
