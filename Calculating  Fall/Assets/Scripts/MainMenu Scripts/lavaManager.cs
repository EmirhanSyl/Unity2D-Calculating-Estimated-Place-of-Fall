using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaManager : MonoBehaviour
{
    GameObject[] lavas;
    float timer;
    public float lavaSetTime;
    bool decrease;
    
    void Start()
    {
        lavas = GameObject.FindGameObjectsWithTag("lava");
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lavaSetTime)
        {
            foreach(GameObject lava in lavas)
            {
                lava.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        if (timer >= lavaSetTime * 2)
        {
            foreach (GameObject lava in lavas)
            {
                lava.GetComponent<SpriteRenderer>().flipX = false;
            }
            timer = 0;
        }
    }
}
